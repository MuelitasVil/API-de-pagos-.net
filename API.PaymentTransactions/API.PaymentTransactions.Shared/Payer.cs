using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static API.PaymentTransactions.Shared.Enums;

namespace API.PaymentTransactions.Shared
{
    public class Payer
    {

        public long PayerId { get; set; }
        public documentTypes documentType { get; set; }
        public string name { get; set; }
        public string number { get; set; }


    }
}
