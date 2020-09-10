using Microsoft.AspNetCore.Mvc;
using mvCommerce.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvCommerce.Libraries.Component
{
    public class MenuViewComponent : ViewComponent
    {
        private ICategoryRepository _categoryRepository;
        public MenuViewComponent(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        //TODO - Logic of Component
        
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categoriesList = _categoryRepository.GetAllCategories().ToList();
            
            return View(categoriesList);
        }
    }
}
