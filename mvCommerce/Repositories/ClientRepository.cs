using mvCommerce.Database;
using mvCommerce.Models;
using mvCommerce.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvCommerce.Repositories
{
    public class ClientRepository : IClientRepository
    {
       private mvCommerceContext _database;
       public ClientRepository(mvCommerceContext database)
       {
            _database = database;
       }
        public Client Login(string email, string password)
        {
            Client client = _database.Client.Where(c => c.Email == email && c.Password == password).FirstOrDefault();
            return client;
        }
       
        public void Register(Client client)
        {
            _database.Add(client);
            _database.SaveChanges();
        }
        public void Update(Client client)
        {
            _database.Update(client);
            _database.SaveChanges();
        }
        public void Delete(int id)
        {
            Client client = GetClient(id);
            _database.Remove(client);
            _database.SaveChanges();
        }
        public Client GetClient(int id)
        {
            return _database.Client.Find(id);
        }
        public IEnumerable<Client> GetAllClients()
        {
            return _database.Client.ToList();
        }
    }
}
