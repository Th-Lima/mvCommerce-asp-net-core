using Microsoft.AspNetCore.Mvc;
using mvCommerce.Libraries.Filter;
using mvCommerce.Libraries.Lang;
using mvCommerce.Models;
using mvCommerce.Models.Constants;
using mvCommerce.Repositories.Contracts;
using X.PagedList;

namespace mvCommerce.Areas.Collaborator.Controllers
{
    [Area("Collaborator")]
    [CollaboratorAuthorization]
    public class ClientController : Controller
    {
        private IClientRepository _clientRepository { get; set; }

        public ClientController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public IActionResult Index(int? page, string search)
        {
            IPagedList<Models.Client> clients = _clientRepository.GetAllClients(page, search);
            return View(clients);
        }

        [ValidateHttpReferer]
        public IActionResult EnableOrDisable(int id)
        {
            Models.Client client = _clientRepository.GetClient(id);
            client.Situation = (client.Situation == SituationConstant.Active) ? SituationConstant.Inactive : SituationConstant.Active;

            _clientRepository.Update(client);

            TempData["MSG_S"] = Message.MSG_S001;


            return RedirectToAction(nameof(Index));
        }
    }
}