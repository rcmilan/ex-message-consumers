using Payment.Worker.Messages;
using Payment.Worker.Models;
using Shared.MQ;
using Shared.MQ.Services;

namespace Payment.Worker.Producers
{
    internal class ReceivedPaymentProducer
    {
        private readonly IMQService _mqService;

        public ReceivedPaymentProducer(IMQService mqService)
        {
            _mqService = mqService;
        }

        public Task NotifyOrderPaid(ReceivedOrderPaymentRequest order)
        {
            if (order.Processed)
            {
                var message = new OrderPaid(order.Id);

                _mqService.Publish(Exchanges.PAYMENT_ACCEPTED, message);

                return Task.FromResult(order);
            }

            return Task.CompletedTask;
        }

    }
}
