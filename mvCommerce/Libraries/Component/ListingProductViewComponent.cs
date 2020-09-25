using Microsoft.AspNetCore.Mvc;
using mvCommerce.Models;
using mvCommerce.Models.ViewModels;
using mvCommerce.Repositories.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mvCommerce.Libraries.Component
{
    public class ListingProductViewComponent : ViewComponent
    {
        private IProductRepository _productRepository;
        private ICategoryRepository _categoryRepository;

        public ListingProductViewComponent(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            int? page = 1;
            string search = "";
            string ordering = "A";
            IEnumerable<Category> categories = null;

            if (HttpContext.Request.Query.ContainsKey("page") && !string.IsNullOrEmpty(HttpContext.Request.Query["page"]))
            { 
                page = int.Parse(HttpContext.Request.Query["page"]);
            }

            if (HttpContext.Request.Query.ContainsKey("search") && !string.IsNullOrEmpty(HttpContext.Request.Query["search"]))
            {
                search = HttpContext.Request.Query["search"].ToString();
            }

            if (HttpContext.Request.Query.ContainsKey("ordering") && !string.IsNullOrEmpty(HttpContext.Request.Query["ordering"]))
            {
                ordering = HttpContext.Request.Query["ordering"].ToString();
            }
            if (ViewContext.RouteData.Values.ContainsKey("slug") && !string.IsNullOrEmpty(ViewContext.RouteData.Values["slug"].ToString()))
            {
                string slug = ViewContext.RouteData.Values["slug"].ToString();
                Category mainCategory = _categoryRepository.GetCategory(slug);
                categories =  _categoryRepository.GetCategoryRecursively(mainCategory);
            }

            var viewModel = new ListingProductViewModel(){ List = _productRepository.GetAllProducts(page, search, ordering, categories) };
           
            return View(viewModel);
        }
    }
}
