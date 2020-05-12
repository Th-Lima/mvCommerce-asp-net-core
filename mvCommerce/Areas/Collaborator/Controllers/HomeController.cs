using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace mvCommerce.Areas.Collaborator.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult RecoverPassword()
        {
            return View();
        }
        public IActionResult CreateNewPassword()
        {
            return View();
        }
    }
}