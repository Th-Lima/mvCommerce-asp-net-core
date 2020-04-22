using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mvCommerce.Libraries.Email;
using mvCommerce.Models;

namespace mvCommerce.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ContactAction()
        {
            Contact contact = new Contact();
            contact.Name = HttpContext.Request.Form["name"];
            contact.Email = HttpContext.Request.Form["email"];
            contact.Text = HttpContext.Request.Form["text"];

            ContactEmail.SendContactPerEmail(contact);

            return new ContentResult() { Content = string.Format("Dados recebidos com sucesso!<br/> Nome: {0} <br/>E-mail: {1} <br/>Texto: {2}", contact.Name, contact.Email, contact.Text), ContentType = "text/html"};
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult RegisterClient() 
        {
            return View();
        }
        public IActionResult ShoppingCart()
        {
            return View();
        }

    }
}