namespace Payment.Worker.Models
{
    internal class ReceivedOrderPaymentRequest
    {
        public Guid Id { get; set; }

        public decimal Total { get; set; }

        public bool Processed { get; set; } = false;

        public bool SetProcessedStatus(bool process)
        {
            Processed = process;

            return Processed;
        }
    }
}
