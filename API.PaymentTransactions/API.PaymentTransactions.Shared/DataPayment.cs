using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.PaymentTransactions.Shared
{
    public class DataPayment
    {
        public Payment payment { get; set; }
        public Mount mount { get; set; }
        public Receipt receipt { get; set; }

    }
}