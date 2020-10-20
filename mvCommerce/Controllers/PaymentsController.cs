using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using mvCommerce.Controllers.Base;
using mvCommerce.Libraries.Manager.Freight;
using mvCommerce.Libraries.ShoppingCart;
using mvCommerce.Models.ProductAggregator;
using mvCommerce.Repositories.Contracts;

namespace mvCommerce.Controllers
{
    public class PaymentsController : BaseController
    {
        public PaymentsController(
       CookieShoppingCart cookieShoppingCart,
       IProductRepository productRepository,
       IMapper mapper,
       WSCorreiosCalculateFreight wSCorreiosCalculateFreight,
       CalculatePackage calculatePackage,
       CookieValueDeadlineFreight cookieValueDeadlineFreight)
      : base(cookieShoppingCart, productRepository, mapper, wSCorreiosCalculateFreight, calculatePackage, cookieValueDeadlineFreight) { }

        public IActionResult Index()
        {
            List<ProductItem> products = LoadProductDb();

            return View(products);
        }
    }
}
