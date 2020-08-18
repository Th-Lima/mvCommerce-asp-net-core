using mvCommerce.Models;

namespace mvCommerce.Repositories.Contracts
{
    public interface IImageRepository
    {
        void Register(Image image);
        void Delete(int id);
        void DeleteImageOfProduct(int productId);
    }
}
