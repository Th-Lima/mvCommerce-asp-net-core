using Microsoft.AspNetCore.Mvc;
using mvCommerce.Libraries.Filter;
using mvCommerce.Libraries.Login;
using mvCommerce.Models;
using mvCommerce.Repositories.Contracts;

namespace mvCommerce.Areas.Client.Controllers
{
    [Area("Client")]
    public class HomeController : Controller
    {
        private IClientRepository _repositoryClient;
        private ClientLogin _clientLogin;
        private IDeliveryAddressRepository _deliveryRepository;

        public HomeController(IClientRepository clientRepository, ClientLogin clientLogin, IDeliveryAddressRepository deliveryRepository)
        {
            _repositoryClient = clientRepository;
            _clientLogin = clientLogin;
            _deliveryRepository = deliveryRepository;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login([FromForm] Models.Client client, string returnUrl = null)
        {
            Models.Client clientDB = _repositoryClient.Login(client.Email, client.Password);
            if (clientDB != null)
            {
                _clientLogin.Login(clientDB);
                if (returnUrl == null)
                {
                    return new RedirectResult(Url.Action(nameof(Panel)));
                }
                else
                {
                    return LocalRedirectPermanent(returnUrl);
                }
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
        public IActionResult RegisterClient([FromForm] Models.Client client, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                _repositoryClient.Register(client);
                _clientLogin.Login(client);

                TempData["MSG_S"] = "Agora você é cadastrado no CompraTudo, aproveite!";

                if (returnUrl == null)
                {
                    return RedirectToAction("Index", "Home", new { area = "" });
                }
                else
                {
                    return LocalRedirectPermanent(returnUrl);
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult RegisterDeliveryAddress()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterDeliveryAddress([FromForm] DeliveryAddress deliveryAddress, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                deliveryAddress.ClientId = _clientLogin.GetClient().Id;
                _deliveryRepository.Register(deliveryAddress);

                if (returnUrl == null)
                {
                    //TODO - LIST OF ADRESSES 
                }
                else
                {
                    return LocalRedirectPermanent(returnUrl);
                }
            }
            return View();
        }
    }
}