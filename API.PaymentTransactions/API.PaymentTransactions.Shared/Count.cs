using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        
    }
}
