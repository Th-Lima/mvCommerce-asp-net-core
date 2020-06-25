using mvCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace mvCommerce.Repositories.Contracts
{
    public interface ICollaboratorRepository
    {
        Collaborator Login(string email, string password);

        void Register(Collaborator collaborator);
        void Update(Collaborator collaborator);
        void UpdatePassword(Collaborator collaborator);
        void Delete(int id);
        Collaborator GetCollaborator(int id);
        IPagedList<Collaborator> GetAllCollaborators(int? page);
    }
}
