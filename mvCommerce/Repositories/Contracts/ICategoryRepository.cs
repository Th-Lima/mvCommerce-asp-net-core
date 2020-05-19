﻿using mvCommerce.Models;
using X.PagedList;

namespace mvCommerce.Repositories.Contracts
{
    public interface ICategoryRepository
    {
        //CRUD
        void Register(Category category);
        void Update(Category category);
        void Delete(int id);
        Category GetCategory(int id);
        IPagedList<Category> GetAllCategories(int? page);
    }
}
