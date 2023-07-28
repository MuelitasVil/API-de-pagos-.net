using static API.PaymentTransactions.Shared.Enums;

namespace API.PaymentTransactions.Shared
{
    public class Payer
    {
        
        public long PayerId { get; set; }
        public documentTypes documentType { get; set; }
        public string email { get; set; }  
        public string name { get; set; }
        public string number { get; set; }


    }
}
