using Microsoft.Extensions.Configuration;
using mvCommerce.Database;
using mvCommerce.Models;
using mvCommerce.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace mvCommerce.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private IConfiguration _config;
       private mvCommerceContext _database;
       public ClientRepository(mvCommerceContext database, IConfiguration config)
       {
            _database = database;
            _config = config;
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
        public IPagedList<Client> GetAllClients(int? page)
        {
            int pageNumber = page ?? 1;
            int registerPerPage = _config.GetValue<int>("RegisterPerPage");

            return _database.Client.ToPagedList<Client>(pageNumber, registerPerPage);
        }
    }
}
