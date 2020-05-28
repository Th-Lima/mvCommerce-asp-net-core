using Microsoft.Extensions.Configuration;
using mvCommerce.Database;
using mvCommerce.Models;
using mvCommerce.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;
using X.PagedList;

namespace mvCommerce.Repositories
{
    public class CollaboratorRepository : ICollaboratorRepository
    {
        private mvCommerceContext _database;
        private IConfiguration _configuration;

        public CollaboratorRepository(mvCommerceContext database, IConfiguration configuration)
        {
            _database = database;
            _configuration = configuration;
        }

        public Collaborator Login(string email, string password)
        {
            Collaborator collaborator = _database.Collaborators.Where(c => c.Email == email && c.Password == password).FirstOrDefault();
            return collaborator;
        }

        //CRUD
        public void Register(Collaborator collaborator)
        {
            _database.Add(collaborator);
            _database.SaveChanges();
        }

        public void Update(Collaborator collaborator)
        {
            _database.Update(collaborator);
            _database.SaveChanges();
        }

        public void Delete(int id)
        {
            Collaborator collaborator = GetCollaborator(id);
            _database.Remove(collaborator);
            _database.SaveChanges();
        }

        public Collaborator GetCollaborator(int id)
        {
            return _database.Collaborators.Find(id);
        }

        public IEnumerable<Collaborator> GetAllCollaborators()
        {
            return _database.Collaborators.ToList();
        }

        public IPagedList<Collaborator> GetAllCollaborators(int? page)
        {
            int pageNumber = page ?? 1;
            int registerPerPage = _configuration.GetValue<int>("RegisterPerPage");
          
            return _database.Collaborators.ToPagedList(pageNumber, registerPerPage);
        }
    }
}
