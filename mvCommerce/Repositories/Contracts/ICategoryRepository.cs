using mvCommerce.Models;
using System.Collections.Generic;
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
        Category GetCategory(string slug);
        IEnumerable<Category> GetCategoryRecursively(Category fatherCategory);
        IEnumerable<Category> GetAllCategories();
        IPagedList<Category> GetAllCategories(int? page);
    }
}
