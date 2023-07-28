using static API.PaymentTransactions.Shared.Enums;

namespace API.PaymentTransactions.Shared
{
    public class Status
    {
        public long statusId { get; set; }
        public PosibleStatus status { get; set; }
        public string reason { get; set; }
        public string message { get; set; }
        public DateTime Date { get; set; }
    }
}
