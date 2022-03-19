using Payment.Worker.Models;
using Payment.Worker.Producers;
using RabbitMQ.Client.Events;
using Shared.MQ;
using Shared.MQ.Services;

namespace Payment.Worker.Consumers
{
    internal class PaymentRequestConsumer : BackgroundService
    {
        private readonly IMQService _mqService;
        private readonly ReceivedPaymentProducer _receivedPaymentProducer;

        public PaymentRequestConsumer(IMQService mqService, ReceivedPaymentProducer receivedPaymentProducer)
        {
            _mqService = mqService;
            _receivedPaymentProducer = receivedPaymentProducer;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = _mqService.CreateEventingBasicConsumer(Exchanges.PAYMENT_REQUEST);

            consumer.Received += (model, ea) =>
            {
                var order = ea.DeserializeDeliveredEventArg<ReceivedOrderPaymentRequest>();

                Console.WriteLine("PEDIDO {0} RECEBIDO!!", order.Id);

                var paymentStatus = Shared.Extensions.FakeProcess();

                order.SetProcessedStatus(paymentStatus);

                if (order.Processed)
                    NotifyOrderPaid(ea, order);
                else
                    Console.WriteLine("NÃO FOI POSSIVEL REALIZAR PAGAMENTO DO PEDIDO {0}!!", order.Id);
            };

            _mqService.BasicConsume(Exchanges.PAYMENT_REQUEST, consumer);

            while (!stoppingToken.IsCancellationRequested)
                Thread.Sleep(1000);

            return Task.CompletedTask;
        }

        private void NotifyOrderPaid(BasicDeliverEventArgs ea, ReceivedOrderPaymentRequest order)
        {
            Console.WriteLine("PEDIDO {0} PAGO COM SUCESSO!!", order.Id);

            _mqService.BasicAck(ea.DeliveryTag);

            Console.WriteLine("ENVIANDO PEDIDO {0} PARA RESTAURANTE!!", order.Id);

            _receivedPaymentProducer.NotifyOrderPaid(order);
        }
    }
}