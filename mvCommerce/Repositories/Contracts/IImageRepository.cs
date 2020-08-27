using mvCommerce.Models;
using System.Collections.Generic;

namespace mvCommerce.Repositories.Contracts
{
    public interface IImageRepository
    {
        void Register(Image image);
        void RegisterImage(List<Image> listImages, int productId);
        void Delete(int id);
        void DeleteImageOfProduct(int productId);
    }
}
