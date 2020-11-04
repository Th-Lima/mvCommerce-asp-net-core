using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using mvCommerce.Controllers.Base;
using mvCommerce.Libraries.Cookie;
using mvCommerce.Libraries.Filter;
using mvCommerce.Libraries.Lang;
using mvCommerce.Libraries.Login;
using mvCommerce.Libraries.Manager.Freight;
using mvCommerce.Libraries.ShoppingCart;
using mvCommerce.Libraries.Text;
using mvCommerce.Models;
using mvCommerce.Models.ProductAggregator;
using mvCommerce.Repositories.Contracts;

namespace mvCommerce.Controllers
{
    [ClientAuthorization]
    public class PaymentsController : BaseController
    {
        private Cookie _cookie;

        public PaymentsController(
            ClientLogin clientLogin,
            CookieShoppingCart cookieShoppingCart,
            IProductRepository productRepository,
            IDeliveryAddressRepository deliveryAddressRepository,
            IMapper mapper,
            WSCorreiosCalculateFreight wSCorreiosCalculateFreight,
            CalculatePackage calculatePackage,
            CookieFreight cookieValueDeadlineFreight,
            Cookie cookie)
        : base(clientLogin, cookieShoppingCart, productRepository, deliveryAddressRepository, mapper, wSCorreiosCalculateFreight, calculatePackage, cookieValueDeadlineFreight)
        {
            _cookie = cookie;
        }

        [ClientAuthorization]
        public IActionResult Index()
        {
            var typeFreightSelectedByUser = _cookie.Consult("cart.typefreight");

            if (typeFreightSelectedByUser != null)
            {
                
                var addressAdressId = int.Parse(_cookie.Consult("ShoppingCart.Address").Replace("-end", ""));
                int cep = 0;
                if (addressAdressId == 0)
                {
                    cep = int.Parse(Mask.Remove(_clientLogin.GetClient().Zipcode));
                }
                else
                {
                    var address = _deliveryAddressRepository.GetAddress(addressAdressId);
                    cep = int.Parse(Mask.Remove(address.Zipcode));
                }
                
                var hashCart = GenerateHashAndSerialize(_cookieShoppingCart.Consult());

                Freight freight =  _cookieFreight.Consult().Where(c => c.CEP == cep && c.ShoppingCartCode == hashCart).FirstOrDefault();

                if (freight != null)
                {
                    ViewBag.Freight = freight.ListValues.Where(f => f.TypeFreight == typeFreightSelectedByUser).FirstOrDefault();
                    List<ProductItem> products = LoadProductDb();
                    return View(products);
                }
            }
            TempData["MSG_E"] = Message.MSG_E009;
            return RedirectToAction("Index", "ShoppingCart");
        }
    }
}
