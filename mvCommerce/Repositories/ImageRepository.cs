using Microsoft.Extensions.Configuration;
using mvCommerce.Database;
using mvCommerce.Models;
using mvCommerce.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace mvCommerce.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private mvCommerceContext _database;
        public ImageRepository(mvCommerceContext database)
        {
            _database = database;
        }
        public void Register(Image image)
        {
            _database.Add(image);
            _database.SaveChanges();
        }
        public void RegisterImage(List<Image> listImages, int productId)
        {
            foreach (var image in listImages)
            {
                Register(image);
            }
        }
        public void Delete(int id)
        {
            Image image = _database.Images.Find(id);
            _database.Remove(image);
            _database.SaveChanges();
        }

        public void DeleteImageOfProduct(int productId)
        {
            List<Image> images = _database.Images.Where(i => i.ProductId == productId).ToList();
            foreach (Image image in images)
            {
                _database.Remove(image);
            }
            _database.SaveChanges();
        }
    }
}
