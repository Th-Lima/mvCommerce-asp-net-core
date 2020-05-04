using mvCommerce.Database;
using mvCommerce.Models;
using mvCommerce.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace mvCommerce.Repositories
{
    public class NewsletterRepository : INewsletterRepository
    {
        private mvCommerceContext _database;

        public NewsletterRepository(mvCommerceContext database)
        {
            _database = database;
        }

        public IEnumerable<NewsletterEmail> GetAllNewsletter()
        {
            return _database.NewsletterEmails.ToList();
        }

        public void Register(NewsletterEmail newsletter)
        {
            _database.NewsletterEmails.Add(newsletter);
            _database.SaveChanges();
        }
    }
}
