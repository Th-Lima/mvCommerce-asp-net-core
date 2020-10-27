using mvCommerce.Models;
using System.Collections.Generic;

namespace mvCommerce.Repositories.Contracts
{
    public interface IDeliveryAddressRepository
    {
        //CRUD
        void Register(DeliveryAddress deliveryAddress);
        void Update(DeliveryAddress deliveryAddress);
        void Delete(int id);
        DeliveryAddress GetAddress(int id);
        IList<DeliveryAddress> GetAllAdressesClient(int ClientId);
    }
}

