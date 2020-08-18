using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using mvCommerce.Database;
using mvCommerce.Models;
using mvCommerce.Repositories.Contracts;
using System.Linq;
using X.PagedList;

namespace mvCommerce.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private IConfiguration _configuration;
        private mvCommerceContext _database;
        public ProductRepository(mvCommerceContext database, IConfiguration configuration)
        {
            _database = database;
            _configuration = configuration;
        }

        public void Register(Product product)
        {
            _database.Add(product);
            _database.SaveChanges();
        }
        public void Update(Product product)
        {
            _database.Update(product);
            _database.SaveChanges();
        }
        public void Delete(int id)
        {
            Product product = GetProduct(id);
            _database.Remove(product);
            _database.SaveChanges();
        }
        public Product GetProduct(int id)
        {
            return _database.Products.Include(p => p.Images).Where(p => p.Id == id).FirstOrDefault();
        }

        public IPagedList<Product> GetAllProducts(int? page, string search)
        {
            int pageNumber = page ?? 1;
            int registerPerPage = _configuration.GetValue<int>("RegisterPerPage");

            var productDatabase = _database.Products.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                productDatabase = productDatabase.Where(p => p.Name.Contains(search.Trim()));
            }

            return productDatabase.Include(p => p.Images).ToPagedList<Product>(pageNumber, registerPerPage);
        }
    }
}
