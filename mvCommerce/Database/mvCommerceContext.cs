using Microsoft.EntityFrameworkCore;
using mvCommerce.Models;

namespace mvCommerce.Database
{
    public class mvCommerceContext : DbContext
    {
        public mvCommerceContext(DbContextOptions<mvCommerceContext> options) : base(options)
        {

        }
        public DbSet<Client> Client { get; set; }
    }
}
