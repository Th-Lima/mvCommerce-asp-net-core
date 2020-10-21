using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mvCommerce.Libraries.Filter;
using mvCommerce.Libraries.Login;
using mvCommerce.Repositories.Contracts;

namespace mvCommerce.Areas.Client.Controllers
{
    [Area("Client")]
    public class HomeController : Controller
    {
        public IClientRepository _repositoryClient;
        public ClientLogin _clientLogin { get; set; }

        public HomeController(IClientRepository clientRepository, ClientLogin clientLogin)
        {
            _repositoryClient = clientRepository;
            _clientLogin = clientLogin;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login([FromForm] Models.Client client)
        {
            Models.Client clientDB = _repositoryClient.Login(client.Email, client.Password);
            if (clientDB != null)
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
        public IActionResult RegisterClient([FromForm] Models.Client client)
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