using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
