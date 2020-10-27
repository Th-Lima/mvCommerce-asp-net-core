using mvCommerce.Database;
using mvCommerce.Models;
using mvCommerce.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using X.PagedList;

namespace mvCommerce.Repositories
{
    public class DeliveryAddressRepository : IDeliveryAddressRepository
    {
        private mvCommerceContext _database;
        
        public DeliveryAddressRepository(mvCommerceContext database)
        {
            _database = database;
        }

        public void Register(DeliveryAddress deliveryAddress)
        {
            _database.Add(deliveryAddress);
            _database.SaveChanges();
        }

        public void Update(DeliveryAddress deliveryAddress)
        {
            _database.Update(deliveryAddress);
            _database.SaveChanges();
        }

        public void Delete(int id)
        {
            DeliveryAddress deliveryAddress = GetAddress(id);
            _database.Remove(deliveryAddress);
            _database.SaveChanges();
        }

        public DeliveryAddress GetAddress(int id)
        {
            return _database.DeliveryAdresses.Find(id);
        }

        public IList<DeliveryAddress> GetAllAdressesClient(int clientId)
        {
            return _database.DeliveryAdresses.Where(d => d.ClientId == clientId).ToList();
        }
    }
}
