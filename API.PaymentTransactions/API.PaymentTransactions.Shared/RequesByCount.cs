namespace API.PaymentTransactions.Shared
{
    public class RequesByCount
    {
        public long RequesByCountId { get; set; }
        public string reference { get; set; }
        public long CountId { get; set; } 
        public long requestId { get; set; }

    }
}
