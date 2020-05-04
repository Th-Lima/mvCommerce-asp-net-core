using mvCommerce.Models;
using System.Collections.Generic;

namespace mvCommerce.Repositories.Contracts
{
    public interface IClientRepository
    {
        Client Login(string email, string senha);

        //CRUD
        void Register(Client client);
        void Update(Client client);
        void Delete(int id);
        Client GetClient(int id);
        IEnumerable<Client> GetAllClients();
    }
}
