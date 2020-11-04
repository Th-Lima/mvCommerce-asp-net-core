using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using mvCommerce.Controllers.Base;
using mvCommerce.Libraries.Filter;
using mvCommerce.Libraries.Lang;
using mvCommerce.Libraries.Login;
using mvCommerce.Libraries.Manager.Freight;
using mvCommerce.Libraries.ShoppingCart;
using mvCommerce.Models;
using mvCommerce.Models.Constants;
using mvCommerce.Models.ProductAggregator;
using mvCommerce.Repositories.Contracts;

namespace mvCommerce.Controllers
{
    public class ShoppingCartController : BaseController
    {

        public ShoppingCartController(
            ClientLogin clientLogin,
            CookieShoppingCart cookieShoppingCart,
            IProductRepository productRepository,
            IDeliveryAddressRepository deliveryAddressRepository,
            IMapper mapper,
            WSCorreiosCalculateFreight wSCorreiosCalculateFreight,
            CalculatePackage calculatePackage,
            CookieFreight cookieFreight
            )
            : base(clientLogin, cookieShoppingCart, productRepository, deliveryAddressRepository, mapper, wSCorreiosCalculateFreight, calculatePackage, cookieFreight) { }

        public IActionResult Index()
        {
            List<ProductItem> productItemComplete = LoadProductDb();

            return View(productItemComplete);
        }

        //Item ID = ID Product
        public IActionResult AddItem(int id)
        {
            Product product = _productRepository.GetProduct(id);
            if (product == null)
            {
                return View("ItemNonexistent");
            }
            else
            {
                var item = new ProductItem() { Id = id, AmountProductsCart = 1 };
                _cookieShoppingCart.Register(item);

                TempData["MSG_S"] = Message.MSG_S004;
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult UpdateAmount(int id, int amount)
        {
            Product product = _productRepository.GetProduct(id);
            if (amount < 1)
            {
                //Error
                return BadRequest(new { message = Message.MSG_E007 });

            }
            else if (amount > product.Amount)
            {
                //Error
                return BadRequest(new { message = Message.MSG_E008 });
            }
            else
            {
                //Ok
                var item = new ProductItem() { Id = id, AmountProductsCart = amount };
                _cookieShoppingCart.Update(item);
                return RedirectToAction(nameof(Index));
            }
        }

        [ClientAuthorization]
        public IActionResult AddressDelivery()
        {
            Client client = _clientLogin.GetClient();

            IList<DeliveryAddress> deliveryAddresses = _deliveryAddressRepository.GetAllAdressesClient(client.Id);

            ViewBag.Products = LoadProductDb();

            ViewBag.Client = client;
            ViewBag.Address = deliveryAddresses;
            return View();
        }

        public IActionResult RemoveItem(int id)
        {
            _cookieShoppingCart.Remove(new ProductItem() { Id = id });
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> CalculateFreight(int cepDestiny)
        {
            try
            {
                //Verify if exists at freight the calculate for same CEP and products
                Freight freight = _cookieFreight.Consult().Where(a => a.CEP == cepDestiny && a.ShoppingCartCode == GenerateHashAndSerialize(_cookieShoppingCart.Consult())).FirstOrDefault();
                if (freight != null)
                {
                    return Ok(freight);
                }
                else
                {
                    List<ProductItem> products = LoadProductDb();

                    List<Package> packages = _calculatePackage.CalculatingPackage(products);

                    ValueDeadlineFreight valuesPAC = await _wSCorreiosCalculateFreight.CalculateFreight(cepDestiny.ToString(), TypeFreightConstant.PAC, packages);
                    ValueDeadlineFreight valuesSEDEX = await _wSCorreiosCalculateFreight.CalculateFreight(cepDestiny.ToString(), TypeFreightConstant.SEDEX, packages);
                    ValueDeadlineFreight valuesSEDEX10 = await _wSCorreiosCalculateFreight.CalculateFreight(cepDestiny.ToString(), TypeFreightConstant.SEDEX10, packages);

                    List<ValueDeadlineFreight> list = new List<ValueDeadlineFreight>();
                    if (valuesPAC != null) list.Add(valuesPAC);
                    if (valuesSEDEX != null) list.Add(valuesSEDEX);
                    if (valuesSEDEX10 != null) list.Add(valuesSEDEX10);


                    freight = new Freight()
                    {
                        CEP = cepDestiny,
                        ShoppingCartCode = GenerateHashAndSerialize(_cookieShoppingCart.Consult()),
                        ListValues = list
                    };

                    _cookieFreight.Register(freight);

                    return Ok(freight);
                }
            }

            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
