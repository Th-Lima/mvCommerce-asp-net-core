using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvCommerce.Libraries.Text
{
    public class Mask
    {
        public static string Remove(string value)
        {
            return value.Replace("(", "").Replace(")", "").Replace("-", "").Replace(".", "");
        }
    }
}
