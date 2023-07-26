using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static API.PaymentTransactions.Shared.Enums;

namespace API.PaymentTransactions.Shared
{
    public class Status
    {
        public PosibleStatus status { get; set; }
        public string reason { get; set; }
        public string message { get; set; }
        public DateTime Date { get; set; }
    }
}
