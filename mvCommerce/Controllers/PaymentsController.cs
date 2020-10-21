using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using mvCommerce.Controllers.Base;
using mvCommerce.Libraries.Cookie;
using mvCommerce.Libraries.Lang;
using mvCommerce.Libraries.Manager.Freight;
using mvCommerce.Libraries.ShoppingCart;
using mvCommerce.Models.ProductAggregator;
using mvCommerce.Repositories.Contracts;

namespace mvCommerce.Controllers
{
    public class PaymentsController : BaseController
    {
        private Cookie _cookie;

        public PaymentsController(
        CookieShoppingCart cookieShoppingCart,
        IProductRepository productRepository,
        IMapper mapper,
        WSCorreiosCalculateFreight wSCorreiosCalculateFreight,
        CalculatePackage calculatePackage,
        CookieValueDeadlineFreight cookieValueDeadlineFreight,
        Cookie cookie)
        : base(cookieShoppingCart, productRepository, mapper, wSCorreiosCalculateFreight, calculatePackage, cookieValueDeadlineFreight)
        {
            _cookie = cookie;
        }

        public IActionResult Index()
        {
            var typeFreightSelectedByUser = _cookie.Consult("cart.typefreight");

            if (typeFreightSelectedByUser != null)
            {
                var freight = _cookieValueDeadlineFreight.Consult().Where(f => f.TypeFreight == typeFreightSelectedByUser).FirstOrDefault();

                if (freight != null)
                {
                    ViewBag.Freight = freight;
                    List<ProductItem> products = LoadProductDb();
                    return View(products);
                }
            }
            TempData["MSG_E"] = Message.MSG_E009;
            return RedirectToAction("Index", "ShoppingCart");
        }
    }
}
