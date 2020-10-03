using mvCommerce.Models;
using mvCommerce.Models.ProductAggregator;
using System.Collections.Generic;
using X.PagedList;

namespace mvCommerce.Repositories.Contracts
{
    public interface IProductRepository
    {
        //CRUD
        void Register(Product client);
        void Update(Product client);
        void Delete(int id);

        Product GetProduct(int id);
        IPagedList<Product> GetAllProducts(int? page, string search);
        IPagedList<Product> GetAllProducts(int? page, string search, string ordering, IEnumerable<Category> categories);
    }
}
