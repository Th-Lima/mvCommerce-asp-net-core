using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using mvCommerce.Libraries.Filter;
using mvCommerce.Models;
using mvCommerce.Repositories.Contracts;
using X.PagedList;

namespace mvCommerce.Areas.Collaborator.Controllers
{
    [Area("Collaborator")]
    //TODO - Enable Authorization
   //[CollaboratorAuthorization]
    public class CategoryController : Controller
    {
        private ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public IActionResult Index(int? page)
        {
            var categories = _categoryRepository.GetAllCategories(page);
            return View(categories);
        }

        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.Categories = _categoryRepository.GetAllCategories().Select(c => new SelectListItem(c.Name, c.Id.ToString()));
            return View();
        }

        [HttpPost]
        public IActionResult Register([FromForm]Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.Register(category);

                TempData["MSG_S"] = "Categoria salva com sucesso";

                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = _categoryRepository.GetAllCategories().Select(c => new SelectListItem(c.Name, c.Id.ToString()));
            return View();
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var categorie = _categoryRepository.GetCategory(id);
            ViewBag.Categories = _categoryRepository.GetAllCategories().Where(c => c.Id != id).Select(c => new SelectListItem(c.Name, c.Id.ToString()));
            return View(categorie);
        }

        [HttpPost]
        public IActionResult Update([FromForm]Category category, int id)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.Update(category);
                TempData["MSG_S"] = "Categoria salva com sucesso";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = _categoryRepository.GetAllCategories().Where(c => c.Id != id).Select(c => new SelectListItem(c.Name, c.Id.ToString()));
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            _categoryRepository.Delete(id);
            
            TempData["MSG_S"] = "Categoria excluída com sucesso";

            return RedirectToAction(nameof(Index));
        }
    }
}