using Microsoft.AspNetCore.Mvc;
using mvCommerce.Libraries.Lang;
using mvCommerce.Libraries.Text;
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
            if (ModelState.IsValid)
            {
                //TODO - generate aleatory password, Submit email

                collaborator.Type = "C";
                _collaboratorRepository.Register(collaborator);

                TempData["MSG_S"] = Message.MSG_S001;

               return RedirectToAction(nameof(Index));
            }
            return View();
        }
        
        [HttpGet]
        public IActionResult GeneratePassword(int id)
        {
             Models.Collaborator collaborator = _collaboratorRepository.GetCollaborator(id);
             collaborator.Password = KeyGenerator.GetUniqueKey(8);
            _collaboratorRepository.Update(collaborator);
            //TODO - Submit email


        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            Models.Collaborator collaborator =  _collaboratorRepository.GetCollaborator(id);
            return View(collaborator);
        }

        [HttpPost]
        public IActionResult Update([FromForm] Models.Collaborator collaborator, int id)
        {
            if (ModelState.IsValid)
            {
                _collaboratorRepository.Update(collaborator);

                TempData["MSG_S"] = Message.MSG_S001;

                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            _collaboratorRepository.Delete(id);

            TempData["MSG_S"] = Message.MSG_S002;

            return RedirectToAction(nameof(Index));
        }
    }
}