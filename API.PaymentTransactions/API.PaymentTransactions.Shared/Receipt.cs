using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using static API.PaymentTransactions.Shared.Enums;

namespace API.PaymentTransactions.Shared
{
    public class Receipt

    {

        public long ReceiptId { get; set; }
        public PaymentFranchise franchise { get; set; }
        public string reference { get; set; }
        public IssuerNames issuerName { get; set; }
        public long authorization { get; set; }
        public PaymentMethods paymentMethod { get; set; }
        public long payerId { get; set; }   
        public long fieldsId { get; set; }
        public long paymentId { get; set; }




    }
}
