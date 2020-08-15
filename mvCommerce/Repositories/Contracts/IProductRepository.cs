using mvCommerce.Models;
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
    }
}
