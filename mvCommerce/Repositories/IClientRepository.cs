using mvCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvCommerce.Repositories
{
   public interface IClientRepository
    {
        Client Login(string email, string senha);

        //CRUD
        void Register(Client client);
        void Update(Client client);
        void Delete(int id);
        Client GetClient(int id);
        List<Client> GetAllClients();
    }
}
