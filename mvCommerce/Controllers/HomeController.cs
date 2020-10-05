using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using mvCommerce.Libraries.Email;
using mvCommerce.Models;
using System.ComponentModel.DataAnnotations;
using System.Text;
using mvCommerce.Repositories.Contracts;
using Microsoft.AspNetCore.Http;
using mvCommerce.Libraries.Login;
using mvCommerce.Libraries.Filter;
using mvCommerce.Models.ViewModels;

namespace mvCommerce.Controllers
{
    public class HomeController : Controller
    {
        private IClientRepository _repositoryClient;
        private INewsletterRepository _repositoryNewsletter;
        private IProductRepository _productRepository;
        private ClientLogin _clientLogin;
        private SendEmail _sendEmail;
      
        public HomeController(
            IClientRepository repository, 
            INewsletterRepository repositoryNewsletter, 
            IProductRepository productRepository, 
            ClientLogin clientLogin, 
            SendEmail sendEmail)
        {
            _repositoryClient = repository;
            _repositoryNewsletter = repositoryNewsletter;
            _clientLogin = clientLogin;
            _sendEmail = sendEmail;
            _productRepository = productRepository;
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

        [HttpGet]
        public IActionResult Category()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
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
                    _sendEmail.SendContactPerEmail(contact);
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
            catch (Exception)
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
            Client clientDB = _repositoryClient.Login(client.Email, client.Password);
            if(clientDB != null)
            {
                _clientLogin.Login(clientDB);
                return new RedirectResult(Url.Action(nameof(Panel)));
            
            }
            else
            {
                ViewData["MSG_E"] = "Algo deu errado, verifique seu email e a senha corretamente!";
                return View();
              
            }
        }

        [HttpGet]
        [ClientAuthorization]
        public IActionResult Panel()
        {
            return new ContentResult() { Content = "Este é o painel do Cliente" };
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
    }
}