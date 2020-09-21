using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace mvCommerce.Models.ViewModels
{
    public class ListingProductViewModel
    {
        public IPagedList<Product> List { get; set; }
        public List<SelectListItem> Ordering
        {
            get
            {
                return new List<SelectListItem>()
                {
                    new SelectListItem("Ordem Alfabética", "A"),
                    new SelectListItem("Menor Preço", "ME"),
                    new SelectListItem("Maior Preço", "MA"),
                };
            }
            private set { }

        }
    }
}
