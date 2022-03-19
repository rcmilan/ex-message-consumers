using Shared.MQ;
using Shared.MQ.Services;

namespace Order.App.Services
{
    public class PaymentService
    {
        private readonly IMQService _mqService;

        public PaymentService(IMQService mqService)
        {
            _mqService = mqService;
        }

        internal Task<Domain.Models.Order> Pay(Domain.Models.Order order)
        {
            _mqService.Publish(Constants.PAYMENT_REQUEST_EXCHANGE, order);

            return Task.FromResult(order);
        }
    }
}