using Microsoft.AspNetCore.Mvc;
using mvCommerce.Models.ViewModels;
using mvCommerce.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvCommerce.Libraries.Component
{
    public class ListingProductViewComponent : ViewComponent
    {
        private IProductRepository _productRepository;
        public ListingProductViewComponent(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            int? page = 1;
            string search = "";
            string ordering = "A";

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

            var viewModel = new ListingProductViewModel(){ List = _productRepository.GetAllProducts(page, search, ordering) };
           
            return View(viewModel);
        }
    }
}
