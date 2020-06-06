using Microsoft.AspNetCore.Mvc;
using mvCommerce.Repositories.Contracts;
using X.PagedList;

namespace mvCommerce.Areas.Collaborator.Controllers
{
    [Area("Collaborator")]
    public class CollaboratorController : Controller
    {
        private ICollaboratorRepository _collaboratorRepository { get; set; }
        
        public CollaboratorController(ICollaboratorRepository collaboratorRepository)
        {
            _collaboratorRepository = collaboratorRepository;
        }
      
        public IActionResult Index(int? page)
        {
            IPagedList<Models.Collaborator> collaborators = _collaboratorRepository.GetAllCollaborators(page);

            return View(collaborators);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register([FromForm] Models.Collaborator collaborator)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Update([FromForm] Models.Collaborator collaborator, int id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View();
        }
    }
}