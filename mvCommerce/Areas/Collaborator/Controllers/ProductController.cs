using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mvCommerce.Repositories.Contracts;

namespace mvCommerce.Areas.Collaborator.Controllers
{
    [Area("Collaborator")]
    public class ProductController : Controller
    {
        private IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public IActionResult Index(int? pageNumber, string search)
        {
            var products = _productRepository.GetAllProducts(pageNumber, search);
            return View(products);
        }
    }
}