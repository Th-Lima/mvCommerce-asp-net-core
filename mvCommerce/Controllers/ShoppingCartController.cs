using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using mvCommerce.Libraries.Lang;
using mvCommerce.Libraries.Manager.Freight;
using mvCommerce.Libraries.ShoppingCart;
using mvCommerce.Models;
using mvCommerce.Models.Constants;
using mvCommerce.Models.ProductAggregator;
using mvCommerce.Repositories.Contracts;

namespace mvCommerce.Controllers
{
    public class ShoppingCartController : Controller
    {
        private ShoppingCart _shoppingCart;
        private IProductRepository _productRepository;
        private IMapper _mapper;
        private WSCorreiosCalculateFreight _wSCorreiosCalculateFreight;
        private CalculatePackage _calculatePackage;

        public ShoppingCartController(
            ShoppingCart shoppingCart,
            IProductRepository productRepository,
            IMapper mapper,
            WSCorreiosCalculateFreight wSCorreiosCalculateFreight,
            CalculatePackage calculatePackage)
        {
            _shoppingCart = shoppingCart;
            _productRepository = productRepository;
            _mapper = mapper;
            _wSCorreiosCalculateFreight = wSCorreiosCalculateFreight;
            _calculatePackage = calculatePackage;
        }

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
                _shoppingCart.Register(item);

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
                _shoppingCart.Update(item);
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult RemoveItem(int id)
        {
            _shoppingCart.Remove(new ProductItem() { Id = id });
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> CalculateFreight(int cepDestiny)
        {
            try
            {
                List<ProductItem> products = LoadProductDb();

                List<Package> packages = _calculatePackage.CalculatingPackage(products);

                ValueDeadlineFreight valuesPAC = await _wSCorreiosCalculateFreight.CalculateFreight(cepDestiny.ToString(), TypeFreightConstant.PAC, packages);
                ValueDeadlineFreight valuesSEDEX = await _wSCorreiosCalculateFreight.CalculateFreight(cepDestiny.ToString(), TypeFreightConstant.SEDEX, packages);
                ValueDeadlineFreight valuesSEDEX10 = await _wSCorreiosCalculateFreight.CalculateFreight(cepDestiny.ToString(), TypeFreightConstant.SEDEX10, packages);

                List<ValueDeadlineFreight> list = new List<ValueDeadlineFreight>();
                if(valuesPAC != null) list.Add(valuesPAC);
                if(valuesSEDEX != null) list.Add(valuesSEDEX);
                if(valuesSEDEX10 != null) list.Add(valuesSEDEX10);

                return Ok(list);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        private List<ProductItem> LoadProductDb()
        {
            List<ProductItem> productItemInCart = _shoppingCart.Consult();
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

    }
}
