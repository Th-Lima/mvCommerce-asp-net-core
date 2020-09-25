using Microsoft.AspNetCore.Mvc;
using mvCommerce.Models;
using mvCommerce.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public IActionResult CategoryList(string slug)
        {
            //TODO - Criar algoritmo recursivo que obtem uma lista com todas as categorias que devem ser utilizadas para apresentar o produto
            Category mainCategory = _categoryRepository.GetCategory(slug);
            List<Category> list = _categoryRepository.GetCategoryRecursively(mainCategory).ToList();

            ViewBag.Categories = list;

            //TODO - Adaptar o produto repository para receber uma lisa de categorias e flitrar os produtos baseado nessa lista
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
