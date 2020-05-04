﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using mvCommerce.Libraries.Email;
using mvCommerce.Models;
using System.ComponentModel.DataAnnotations;
using System.Text;
using mvCommerce.Database;
using mvCommerce.Repositories;

namespace mvCommerce.Controllers
{
    public class HomeController : Controller
    {
        private IClientRepository _repository;
        public HomeController(IClientRepository repository)
        {
            _repository = repository;
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
                //_database.NewsletterEmails.Add(newsletter);
                //_database.SaveChanges();

                //TempData["MSG_S"] = "PRONTO! Agora você irá receber nossas promoções diárias, fique ligado!";

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
        public IActionResult Login()
        {
            return View();
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
                _repository.Register(client);

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