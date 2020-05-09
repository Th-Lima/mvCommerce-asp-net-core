using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using mvCommerce.Libraries.Email;
using mvCommerce.Models;
using System.ComponentModel.DataAnnotations;
using System.Text;
using mvCommerce.Database;
using mvCommerce.Repositories.Contracts;
using Microsoft.AspNetCore.Http;

namespace mvCommerce.Controllers
{
    public class HomeController : Controller
    {
        private IClientRepository _repositoryClient;
        private INewsletterRepository _repositoryNewsletter;
        public HomeController(IClientRepository repository, INewsletterRepository repositoryNewsletter)
        {
            _repositoryClient = repository;
            _repositoryNewsletter = repositoryNewsletter;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
       
        [HttpPost]
        public IActionResult Index([FromForm]NewsletterEmail newsletter)
        {
            if (ModelState.IsValid)
            {
                _repositoryNewsletter.Register(newsletter);

                TempData["MSG_S"] = "PRONTO! Agora você irá receber nossas promoções diárias, fique ligado!";

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }            
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
                    ViewData["CONTACT"] = contact;
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

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login([FromForm]Client client)
        {
            if(client.Email == "lthales53@gmail.com" && client.Password == "1234567")
            {
                HttpContext.Session.Set("ID", new byte[] { 52 });
                HttpContext.Session.SetString("Email", client.Email);

                return new ContentResult() { Content = "Logado!" };
            
            }
            else
            {
                return new ContentResult() { Content = "Não logado" };
              
            }
        }

        public IActionResult Panel()
        {
            byte[] userId;
            if (HttpContext.Session.TryGetValue("ID", out userId))
            {
                return new ContentResult() { Content = "Usuário " + userId[0] + "." + "Email: " + HttpContext.Session.GetString("Email") + " logado!" };
            }
            else
            {
                return new ContentResult() { Content = "Acesso negado!" };
            }
        }

        [HttpGet]
        public IActionResult RegisterClient() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterClient([FromForm] Client client)
        {
            if (ModelState.IsValid)
            {
                _repositoryClient.Register(client);

                TempData["MSG_S"] = "Agora você é cadastrado no mvCommerce, aproveite!";

                //TODO - Implement difference redirects (Painel, Carrinho de Compras etc.)
                return RedirectToAction(nameof(RegisterClient));
            }
            return View();
        }
        public IActionResult ShoppingCart()
        {
            return View();
        }

    }
}