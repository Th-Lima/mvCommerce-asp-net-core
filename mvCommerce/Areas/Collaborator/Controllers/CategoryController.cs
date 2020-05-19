using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
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
            return View();
        }

        [HttpPost]
        public IActionResult Register([FromForm]Category category)
        {
            //TODO - Implement
            return View();
        }

        [HttpGet]
        public IActionResult Update()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Update([FromForm]Category category)
        {
            //TODO - Implement
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View();
        }
    }
}