using Microsoft.AspNetCore.Mvc;
using mvCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvCommerce.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult Visualize()
        {
           Product product =  GetProduct();


            return View(product);
        }
        private Product GetProduct()
        {
            return new Product()
            {
                Id = 1,
                Name = "TV",
                Price = 1500.00,
                Description = "LED 40 POLEGADAS"
            };
        }
    }
}
