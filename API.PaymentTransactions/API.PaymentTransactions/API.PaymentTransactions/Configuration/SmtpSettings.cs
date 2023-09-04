﻿namespace API.PaymentTransactions.API.Configuration
{
    public class SmtpSettings
    {

        public String Server { get; set; }
        public string Port { get; set; }
        public string SenderEmail { get; set; }
        public string SenderName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

    }
}
