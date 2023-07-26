using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static API.PaymentTransactions.Shared.Enums;

namespace API.PaymentTransactions.Shared
{
    public class Mount
    {
        public long MountId { get; set; }

        public long toTotal { get; set; }

        public currencys toCurrency { get; set; } = currencys.COP;

        public long fromTotal { get; set; }
        
        public currencys fromCurrency {
            get => fromCurrency;
            set
            {
                switch (this.fromCurrency)
                {
                    case currencys.COP:
                        this.factor = 1;
                        break;

                    case currencys.USD:
                        this.factor = 3949.19; 
                        break;

                    case currencys.EUR:
                        this.factor = 4378.87;
                        break;

                }
            }
            }


        public double factor;

        public long countId; 




    }
}
