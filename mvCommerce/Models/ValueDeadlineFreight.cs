using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvCommerce.Models
{
    public class ValueDeadlineFreight
    {
        public string TypeFreight { get; set; }
        public string ServiceCode { get; set; }
        public double Value { get; set; }
        public int Deadline { get; set; }
    }
}
