namespace API.PaymentTransactions.Shared
{
    internal class Request
    {
        public string locale { get; set; }
        public string IPAddress { get; set; }
        public string UserAgent { get; set; }
        public DateTime Date { get; set; }
        public string subsription { get; set; }
        public long prayerId { get; set; }
        public long statusId { get; set; }
        public long fieldsId { get; set; }
    }
}
