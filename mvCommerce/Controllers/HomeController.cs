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

namespace mvCommerce.Controllers
{
    public class HomeController : Controller
    {
        private INewsletterRepository _repositoryNewsletter;
        private IProductRepository _productRepository;
        private SendEmail _sendEmail;

        public HomeController(
            INewsletterRepository repositoryNewsletter,
            IProductRepository productRepository,
            SendEmail sendEmail)
        {
            _repositoryNewsletter = repositoryNewsletter;
            _sendEmail = sendEmail;
            _productRepository = productRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index([FromForm] NewsletterEmail newsletter)
        {
            if (ModelState.IsValid)
            {
                _repositoryNewsletter.Register(newsletter);

                TempData["MSG_S_Newsletter"] = "PRONTO! Agora você irá receber nossas promoções diárias, fique ligado!";

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

    }
}