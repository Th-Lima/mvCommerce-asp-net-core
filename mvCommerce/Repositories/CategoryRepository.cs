using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using mvCommerce.Database;
using mvCommerce.Models;
using mvCommerce.Repositories.Contracts;
using System.Collections.Generic;
using X.PagedList;

namespace mvCommerce.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private IConfiguration _configuration;
        private mvCommerceContext _database;
        public CategoryRepository(mvCommerceContext database, IConfiguration configuration)
        {
            _database = database;
            _configuration = configuration;
        }

        public void Register(Category category)
        {
            _database.Add(category);
            _database.SaveChanges();
        }

        public void Update(Category category)
        {
            _database.Update(category);
            _database.SaveChanges();
        }

        public void Delete(int id)
        {
           Category category = GetCategory(id);
            _database.Remove(category);
            _database.SaveChanges();
        }

        public Category GetCategory(int id)
        {
            return _database.Categories.Find(id);
        }

        public IPagedList<Category> GetAllCategories(int? page)
        {
            int pageNumber = page ?? 1;
            int registerPerPage = _configuration.GetValue<int>("RegisterPerPage");
            
            return _database.Categories.Include(a => a.CategoryFather).ToPagedList(pageNumber, registerPerPage);
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _database.Categories;
        }
    }
}
