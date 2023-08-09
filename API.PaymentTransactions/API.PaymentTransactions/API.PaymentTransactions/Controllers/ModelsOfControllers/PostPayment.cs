using API.PaymentTransactions.Shared;
using static API.PaymentTransactions.Shared.Enums;

namespace API.PaymentTransactions.API.Controllers.ModelsOfControllers
{
    public class PostPayment
    {
        public String paymentDescription { get; set; }
        public Double mountFromTotal { get; set; }
        public currencys mountfromCurrency { get; set; }
        public Double mountCountId { get; set; }
        public int mountFactor { get; set; }
        public String receiptReference { get; set; }
        public PaymentFranchise receiptFranchise { get; set; }
        public int receiptAuthorization { get; set; }
        public IssuerNames receiptIssuername { get; set; }
        public PaymentMethods receiptPaymentMethod { get; set; }
        public Double receiptPayerId { get; set; }

    }
}

