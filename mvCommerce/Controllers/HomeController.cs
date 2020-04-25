using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mvCommerce.Libraries.Email;
using mvCommerce.Models;
using System.ComponentModel.DataAnnotations;
using System.Text;

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
            try
            {
                Contact contact = new Contact();
                contact.Name = HttpContext.Request.Form["name"];
                contact.Email = HttpContext.Request.Form["email"];
                contact.Text = HttpContext.Request.Form["text"];

                var listMessages = new List<ValidationResult>();
                var context = new ValidationContext(contact);
                bool isValid = Validator.TryValidateObject(contact, context, listMessages, true);
                if (isValid)
                {
                    ContactEmail.SendContactPerEmail(contact);
                    ViewData["MSG_S"] = "Mensagem de contato enviada com sucesso!";
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var text in listMessages)
                    {
                        sb.Append(text.ErrorMessage + "<br />");
                    }

                    ViewData["MSG_E"] = sb.ToString();
                }
            }
            catch (Exception e)
            {
                ViewData["MSG_E"] = "Oops! tivemos um erro, tente novamente mais tarde!";

                //TODO - Implement log

            }
           
            return View("Contact");
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