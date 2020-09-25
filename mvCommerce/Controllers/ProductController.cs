using Microsoft.AspNetCore.Mvc;
using mvCommerce.Repositories.Contracts;

namespace mvCommerce.Controllers
{
    public class ProductController : Controller
    {
        public ICategoryRepository _categoryRepository;
        public IProductRepository _productRepository;
       

        public ProductController(ICategoryRepository categoryRepository, IProductRepository productRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }

        [HttpGet]
        [Route("Product/Category/{slug}")]
        public IActionResult CategoryList()
        {
            return View();
        }
        
       
        //public ActionResult Visualize()
        //{
        //   Product product =  GetProduct();


        //    return View(product);
        //}
        ////private Product GetProduct()
        ////{
        ////    //return new Product()
        ////    //{
        ////    //    Id = 1,
        ////    //    Name = "TV",
        ////    //    Price = 1500.00,
        ////    //    Description = "LED 40 POLEGADAS"
        ////    //};
        ////}
    }
}
