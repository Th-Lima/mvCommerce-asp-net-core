using Microsoft.AspNetCore.Mvc;
using mvCommerce.Libraries.Filter;
using mvCommerce.Libraries.Login;
using mvCommerce.Repositories.Contracts;

namespace mvCommerce.Areas.Collaborator.Controllers
{
    [Area("Collaborator")]
    public class HomeController : Controller
    {
        private ICollaboratorRepository _collaboratorRepository;
        private CollaboratorLogin _collaboratorLogin;
        public HomeController(ICollaboratorRepository collaboratorRepository, CollaboratorLogin collaboratorLogin)
        {
            _collaboratorRepository = collaboratorRepository;
            _collaboratorLogin = collaboratorLogin;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login([FromForm]Models.Collaborator collaborator)
        {
            Models.Collaborator collaboratorDB = _collaboratorRepository.Login(collaborator.Email, collaborator.Password);
            if (collaboratorDB != null)
            {
                _collaboratorLogin.Login(collaboratorDB);
                return new RedirectResult(Url.Action(nameof(Panel)));

            }
            else
            {
                ViewData["MSG_E"] = "Algo deu errado, verifique seu email e a senha corretamente!";
                return View();

            }
        }

        [CollaboratorAuthorization]
        public IActionResult Logout()
        {
            _collaboratorLogin.Logout();
            return RedirectToAction("Login", "Home");
        }
        
        public IActionResult RecoverPassword()
        {
            return View();
        }
        public IActionResult CreateNewPassword()
        {
            return View();
        }

        [CollaboratorAuthorization]
        public IActionResult Panel()
        {
            return View();
        }
    }
}