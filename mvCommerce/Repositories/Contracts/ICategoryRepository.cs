using mvCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvCommerce.Repositories.Contracts
{
    public interface ICategoryRepository
    {
        //CRUD
        void Register(Category category);
        void Update(Category category);
        void Delete(int id);
        Category GetCategory(int id);
        IEnumerable<Category> GetAllCategories();
    }
}
