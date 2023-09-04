namespace API.PaymentTransactions.API.Configuration
{
    public class JWTConfig
    {
        public String secret { get; set; }  
        public TimeSpan ExpiryTime { get; set; }
    }
}
