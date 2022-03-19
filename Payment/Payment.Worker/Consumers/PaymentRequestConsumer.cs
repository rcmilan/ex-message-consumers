using Payment.Worker.Models;
using Shared;
using Shared.MQ;
using Shared.MQ.Services;

namespace Payment.Worker.Consumers
{
    internal class PaymentRequestConsumer : BackgroundService
    {
        private readonly IMQService _mqService;

        public PaymentRequestConsumer(IMQService mqService)
        {
            _mqService = mqService;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = _mqService.CreateEventingBasicConsumer(Exchanges.PAYMENT_REQUEST);

            consumer.Received += (model, ea) =>
            {
                var order = ea.DeserializeDeliveredEventArg<ReceivedOrderPaymentRequest>();

                Console.WriteLine("PEDIDO {0} RECEBIDO!!", order.Id);
                _mqService.BasicAck(ea.DeliveryTag);

                Console.WriteLine("ENVIANDO PEDIDO {0} PARA RESTAURANTE!!", order.Id);

                FakeLoader();

                order.SetProcessedStatus(true);
            };

            _mqService.BasicConsume(Exchanges.PAYMENT_REQUEST, consumer);

            while (!stoppingToken.IsCancellationRequested)
                Thread.Sleep(1000);

            return Task.CompletedTask;
        }

        private static void FakeLoader()
        {
            var dots = new Random();

            Shared.Extensions.Loader(dots.Next(3, 10));
        }
    }
}