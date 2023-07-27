using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.PaymentTransactions.Shared
{
    public class Field
    {
        public long fieldId { get; set; }
        public String keyWord {  get; set; }
        public long Value { get; set; }
        public bool displayON { get; set; }
        public long fieldsId { get; set; } 
    }
}
