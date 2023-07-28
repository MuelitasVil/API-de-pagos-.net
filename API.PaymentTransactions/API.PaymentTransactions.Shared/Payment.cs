using System;
using System.Collections.Generic;
using System.Linq;
namespace API.PaymentTransactions.Shared
{
    public class Payment
    {
        public long paymentId { get; set; }
        public String description { get; set; }
        public long mountId { get; set; }
        public long statusId { get; set; }
        public long countId { get; set; }
        

    }
}
