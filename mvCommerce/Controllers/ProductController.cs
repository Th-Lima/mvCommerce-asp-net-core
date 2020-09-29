using Microsoft.AspNetCore.Mvc;
using mvCommerce.Models;
using mvCommerce.Repositories.Contracts;

namespace mvCommerce.Controllers
{
    public class ProductController : Controller
    {
        public ICategoryRepository _categoryRepository;
        public IProductRepository _productRepository;
       

        public ProductController(ICategoryRepository categoryRepository, IProductRepository productRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }

        [HttpGet]
        [Route("Product/Category/{slug}")]
        public IActionResult CategoryList(string slug)
        {
            return View(_categoryRepository.GetCategory(slug));
        }

        [HttpGet]
        public ActionResult Visualize(int id)
        {
            return View(_productRepository.GetProduct(id));
        }
    }
}
