using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvCommerce.Models.ProductAggregator
{
    public class ProductItem : Product
    {
        /*
         * Stores the quantity of products that the user intends to buy this item
         */
        public int AmountProductsCart { get; set; }
    }
}
