using Microsoft.AspNetCore.Mvc;
using mvCommerce.Libraries.Email;
using mvCommerce.Libraries.Filter;
using mvCommerce.Libraries.Lang;
using mvCommerce.Libraries.Text;
using mvCommerce.Models.Constants;
using mvCommerce.Repositories.Contracts;
using X.PagedList;

namespace mvCommerce.Areas.Collaborator.Controllers
{
    [Area("Collaborator")]
    [CollaboratorAuthorization(CollaboratorTypeConstant.Manager)]
    public class CollaboratorController : Controller
    {
        private ICollaboratorRepository _collaboratorRepository;
        private SendEmail _sendEmail;
        
        public CollaboratorController(ICollaboratorRepository collaboratorRepository, SendEmail sendEmail)
        {
            _collaboratorRepository = collaboratorRepository;
            _sendEmail = sendEmail;
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
            ModelState.Remove("Password");
            if (ModelState.IsValid)
            {
                collaborator.Type = CollaboratorTypeConstant.Comum;
                collaborator.Password = KeyGenerator.GetUniqueKey(8);
                _collaboratorRepository.Register(collaborator);

                _sendEmail.SendPasswordPerEmail(collaborator);

                TempData["MSG_S"] = Message.MSG_S001;

               return RedirectToAction(nameof(Index));
            }
            return View();
        }
        
        [HttpGet]
        [ValidateHttpReferer]
        public IActionResult GeneratePassword(int id)
        {
             Models.Collaborator collaborator = _collaboratorRepository.GetCollaborator(id);
             collaborator.Password = KeyGenerator.GetUniqueKey(8);
            _collaboratorRepository.UpdatePassword(collaborator);
            
            //send email
            _sendEmail.SendPasswordPerEmail(collaborator);

            TempData["MSG_S"] = Message.MSG_S003;

            return RedirectToAction(nameof(Index));
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
            ModelState.Remove("Password");
            if (ModelState.IsValid)
            {
                _collaboratorRepository.Update(collaborator);

                TempData["MSG_S"] = Message.MSG_S001;

                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        [HttpGet]
        [ValidateHttpReferer]
        public IActionResult Delete(int id)
        {
            _collaboratorRepository.Delete(id);

            TempData["MSG_S"] = Message.MSG_S002;

            return RedirectToAction(nameof(Index));
        }
    }
}