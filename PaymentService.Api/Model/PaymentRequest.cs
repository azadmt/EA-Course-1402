namespace PaymentService.Api.Model
{
    public class PaymentRequest
    {
        public Guid RequesterId { get; set; }

        public Guid OrderId { get; set; }
        public decimal Amount { get; set; }
    }
}