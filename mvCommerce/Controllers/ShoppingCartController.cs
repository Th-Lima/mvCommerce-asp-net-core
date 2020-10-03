﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mvCommerce.Libraries.ShoppingCart;
using mvCommerce.Models;
using mvCommerce.Models.ProductAggregator;
using mvCommerce.Repositories.Contracts;

namespace mvCommerce.Controllers
{
    public class ShoppingCartController : Controller
    {
        private ShoppingCart _shoppingCart;
        private IProductRepository _productRepository;

        public ShoppingCartController (ShoppingCart shoppingCart, IProductRepository productRepository)
        {
            _shoppingCart = shoppingCart;
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            List<ProductItem> productItemInCart = _shoppingCart.Consult();
            List<ProductItem> productItemComplete = new List<ProductItem>();

            foreach (var item in productItemInCart)
            {
                //TODO - Implement AutoMapper
                Product product =  _productRepository.GetProduct(item.Id);

                ProductItem productItem = new ProductItem();
                productItem.Id = product.Id;
                productItem.Name = product.Name;
                productItem.Description = product.Description;
                productItem.Images = product.Images;
                productItem.Price = product.Price;
                productItem.AmountProductsCart = item.AmountProductsCart;

                productItemComplete.Add(productItem);
            }

            return View();
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
                _shoppingCart.Register(item);

                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult UpdateAmount(int id, int amount)
        {
            var item = new ProductItem() { Id = id, AmountProductsCart = amount };
            _shoppingCart.Update(item);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoveItem(int id)
        {
            _shoppingCart.Remove(new ProductItem() { Id = id });
            return RedirectToAction(nameof(Index));
        }
    }
}
