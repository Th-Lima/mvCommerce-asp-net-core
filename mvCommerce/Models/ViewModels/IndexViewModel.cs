using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace mvCommerce.Models.ViewModels
{
    public class IndexViewModel
    {
        public NewsletterEmail Newsletter { get; set; }
        public IPagedList<Product> List { get; set; }
    }
}
