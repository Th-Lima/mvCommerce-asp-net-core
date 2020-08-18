using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using mvCommerce.Repositories.Contracts;
using System.Linq;

namespace mvCommerce.Areas.Collaborator.Controllers
{
    [Area("Collaborator")]
    public class ProductController : Controller
    {
        private IProductRepository _productRepository;
        private ICategoryRepository _categoryRepository;
        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }
        public IActionResult Index(int? pageNumber, string search)
        {
            var products = _productRepository.GetAllProducts(pageNumber, search);
            return View(products);
        }
        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.Categories = _categoryRepository.GetAllCategories().Select(c => new SelectListItem(c.Name, c.Id.ToString()));
            return View();
        }
    }
}