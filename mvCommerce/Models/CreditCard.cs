using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvCommerce.Models
{
    public class CreditCard
    {
        public string CardNumber { get; set; }
        public string NameInCard { get; set; }
        public string Expiration { get; set; }
        public string SecurityCode { get; set; }
    }
}
