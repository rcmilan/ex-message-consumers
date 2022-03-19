using Shared;

namespace Payment.Worker.Messages
{
    internal class OrderPaid : IBaseEvent
    {
        public OrderPaid(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }

        public string Description => $"[{DateTime.Now}] {Id}";
    }
}
