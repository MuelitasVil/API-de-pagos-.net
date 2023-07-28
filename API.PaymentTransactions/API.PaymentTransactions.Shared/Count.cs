using static API.PaymentTransactions.Shared.Enums;

namespace API.PaymentTransactions.Shared
{
    public class Count
    {
        public long countId {  get; set; }
        public currencys currency { get; set; }
        public long Total { get; set; }
        public bool allowPartial { get; set; }
        public bool suscribe { get; set; }
        public long payerId { get; set; }
        public bool paid { get; set; }
        
    }
}
