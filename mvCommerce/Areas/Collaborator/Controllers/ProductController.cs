using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using mvCommerce.Libraries.Files;
using mvCommerce.Libraries.Filter;
using mvCommerce.Libraries.Lang;
using mvCommerce.Models;
using mvCommerce.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace mvCommerce.Areas.Collaborator.Controllers
{
    [Area("Collaborator")]
    [CollaboratorAuthorization]
    public class ProductController : Controller
    {
        private IProductRepository _productRepository;
        private ICategoryRepository _categoryRepository;
        private IImageRepository _imageRepository;
        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository, IImageRepository imageRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _imageRepository = imageRepository;
        }
        public IActionResult Index(int? page, string search)
        {
            var products = _productRepository.GetAllProducts(page, search);
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
                List<Image> listImagesDefinitives = FileManager.MoveProductImage(new List<string>(Request.Form["image"]), product.Id);
                _imageRepository.RegisterImage(listImagesDefinitives, product.Id);

                TempData["MSG_S"] = Message.MSG_S001;
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.Categories = _categoryRepository.GetAllCategories().Select(c => new SelectListItem(c.Name, c.Id.ToString()));
                product.Images = new List<string>(Request.Form["image"]).Where(i => i.Trim().Length > 0).Select(i => new Image() { Path = i }).ToList();

                return View(product);
            }
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

                List<Image> listImagesDefinitives = FileManager.MoveProductImage(new List<string>(Request.Form["image"]), product.Id);
                _imageRepository.DeleteImageOfProduct(product.Id);
                _imageRepository.RegisterImage(listImagesDefinitives, product.Id);
                

                TempData["MSG_S"] = Message.MSG_S001;
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.Categories = _categoryRepository.GetAllCategories().Select(c => new SelectListItem(c.Name, c.Id.ToString()));
                product.Images = new List<string>(Request.Form["image"]).Where(i => i.Trim().Length > 0).Select(i => new Image() { Path = i }).ToList();

                return View(product);
            }
        }
        [HttpGet]
        [ValidateHttpReferer]
        public IActionResult Delete(int id)
        {
            Product product = _productRepository.GetProduct(id);
            
            FileManager.DeleteAllProductsImages(product.Images.ToList());
            
            _imageRepository.DeleteImageOfProduct(id);
            
            _productRepository.Delete(id);
            
            TempData["MSG_S"] = Message.MSG_S002;
            
            return RedirectToAction(nameof(Index));
        }
    }
}