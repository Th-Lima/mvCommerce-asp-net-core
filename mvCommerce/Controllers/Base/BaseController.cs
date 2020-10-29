using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.WebEncoders.Testing;
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
        protected CookieShoppingCart _cookieShoppingCart;
        protected IProductRepository _productRepository;
        protected IMapper _mapper;
        protected WSCorreiosCalculateFreight _wSCorreiosCalculateFreight;
        protected CalculatePackage _calculatePackage;
        protected CookieFreight _cookieFreight;

        public BaseController(
             CookieShoppingCart cookieShoppingCart,
            IProductRepository productRepository,
            IMapper mapper,
            WSCorreiosCalculateFreight wSCorreiosCalculateFreight,
            CalculatePackage calculatePackage,
            CookieFreight cookieFreight)
        {
            _cookieShoppingCart = cookieShoppingCart;
            _productRepository = productRepository;
            _mapper = mapper;
            _wSCorreiosCalculateFreight = wSCorreiosCalculateFreight;
            _calculatePackage = calculatePackage;
            _cookieFreight = cookieFreight;
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
