using mvCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvCommerce.Repositories.Contracts
{
    public interface INewsletterRepository
    {
        void Register(NewsletterEmail newsletter);
        IEnumerable<NewsletterEmail> GetAllNewsletter();
    }
}
