using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using mvCommerce.Libraries.Login;
using mvCommerce.Libraries.Manager.Freight;
using mvCommerce.Libraries.ShoppingCart;
using mvCommerce.Libraries.Text;
using mvCommerce.Models.ProductAggregator;
using mvCommerce.Repositories.Contracts;
using Newtonsoft.Json;

namespace mvCommerce.Controllers.Base
{
    public class BaseController : Controller
    {
        protected ClientLogin _clientLogin;
        protected CookieShoppingCart _cookieShoppingCart;
        protected IProductRepository _productRepository;
        protected IDeliveryAddressRepository _deliveryAddressRepository;
        protected IMapper _mapper;
        protected WSCorreiosCalculateFreight _wSCorreiosCalculateFreight;
        protected CalculatePackage _calculatePackage;
        protected CookieFreight _cookieFreight;

        public BaseController(
            ClientLogin clientLogin,
             CookieShoppingCart cookieShoppingCart,
            IProductRepository productRepository,
            IDeliveryAddressRepository deliveryAddressRepository,
            IMapper mapper,
            WSCorreiosCalculateFreight wSCorreiosCalculateFreight,
            CalculatePackage calculatePackage,
            CookieFreight cookieFreight)
        {
            _clientLogin = clientLogin;
            _cookieShoppingCart = cookieShoppingCart;
            _productRepository = productRepository;
            _mapper = mapper;
            _wSCorreiosCalculateFreight = wSCorreiosCalculateFreight;
            _calculatePackage = calculatePackage;
            _cookieFreight = cookieFreight;
            _deliveryAddressRepository = deliveryAddressRepository;
        }

        protected List<ProductItem> LoadProductDb()
        {
            List<ProductItem> productItemInCart = _cookieShoppingCart.Consult();
            List<ProductItem> productItemComplete = new List<ProductItem>();

            foreach (var item in productItemInCart)
            {
                Product product = _productRepository.GetProduct(item.Id);

                ProductItem productItem = _mapper.Map<ProductItem>(product);
                productItem.AmountProductsCart = item.AmountProductsCart;

                productItemComplete.Add(productItem);
            }

            return productItemComplete;
        }

        protected string GenerateHashAndSerialize(object obj)
        {
            return StringMD5.MD5Hash(JsonConvert.SerializeObject(obj));
        }
    }
}
