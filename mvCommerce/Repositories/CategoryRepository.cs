﻿using mvCommerce.Database;
using mvCommerce.Models;
using mvCommerce.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvCommerce.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        mvCommerceContext _database;
        public CategoryRepository(mvCommerceContext database)
        {
            _database = database;
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

        public IEnumerable<Category> GetAllCategories()
        {
            return _database.Categories.ToList();
        }    
    }
}
