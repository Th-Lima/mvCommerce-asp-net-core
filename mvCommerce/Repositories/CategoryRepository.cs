using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using mvCommerce.Database;
using mvCommerce.Models;
using mvCommerce.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;
using X.PagedList;

namespace mvCommerce.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private IConfiguration _configuration;
        private mvCommerceContext _database;
        private List<Category> _listCategoryRecursively = new List<Category>();
        private List<Category> _categories;
        
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
        
        /// <summary>
        /// Get category find by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Category GetCategory(int id)
        {
            return _database.Categories.Find(id);
        }
        
        /// <summary>
        /// Get category find by slug
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        public Category GetCategory(string slug)
        {
            return _database.Categories.Where(c => c.Slug == slug).FirstOrDefault();
        }

        /// <summary>
        /// Get Categories recursively
        /// </summary>
        /// <param name="fatherCategory"></param>
        /// <returns></returns>
        public IEnumerable<Category> GetCategoryRecursively(Category fatherCategory)
        {
            if(_categories == null) _categories = GetAllCategories().ToList();

            var childCategory = _categories.Where(c => c.CategoryFatherId == fatherCategory.Id);
          
            if (!_listCategoryRecursively.Exists(c => c.Id == fatherCategory.Id))
            {
                _listCategoryRecursively.Add(fatherCategory);
            }
            
           
            if (childCategory.Count() > 0)
            {
                _listCategoryRecursively.AddRange(childCategory.ToList());
                foreach (var category in childCategory)
                {
                    GetCategoryRecursively(category);
                }
            }

            return _listCategoryRecursively;
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
