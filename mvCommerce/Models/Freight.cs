using System.Collections.Generic;

namespace mvCommerce.Models
{
    public class Freight
    {
        public string CEP { get; set; }
        
        //HashCode MDS
        public string ShoppingCartCode { get; set; }
       
        public List<ValueDeadlineFreight> ListValues { get; set; }
    }
}
