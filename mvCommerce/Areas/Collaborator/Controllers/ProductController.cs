using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using mvCommerce.Libraries.Files;
using mvCommerce.Libraries.Lang;
using mvCommerce.Models;
using mvCommerce.Repositories.Contracts;
using System.Collections.Generic;
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
        [HttpPost]
        public IActionResult Register(Product product)
        {
            if (ModelState.IsValid)
            {
                _productRepository.Register(product);

                
                List<string>ListDefinitivePath = FileManager.MoveProductImage(new List<string>(Request.Form["image"]), product.Id.ToString());
                //TODO - Caminho temporario e mover para caminho definitivo
                
                //TODO - Salvar o caminho da imagem no banco de dados.


                TempData["MSG_S"] = Message.MSG_S001;
                return RedirectToAction(nameof(Index));
            }
           
            ViewBag.Categories = _categoryRepository.GetAllCategories().Select(c => new SelectListItem(c.Name, c.Id.ToString()));
            return View();
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            ViewBag.Categories = _categoryRepository.GetAllCategories().Select(c => new SelectListItem(c.Name, c.Id.ToString()));
            Product product = _productRepository.GetProduct(id);
            return View(product);
        }
        [HttpPost]
        public IActionResult Update(Product product, int id)
        {
            if (ModelState.IsValid)
            {
                _productRepository.Update(product);
                TempData["MSG_S"] = Message.MSG_S001;

                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = _categoryRepository.GetAllCategories().Select(c => new SelectListItem(c.Name, c.Id.ToString()));
            return View();
        }
    }
}