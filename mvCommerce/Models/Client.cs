using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvCommerce.Models
{
    public class Client
    {
        // PK
        public int Id { get; set; }

        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string CPF { get; set; }
        public string Sex { get; set; }
        public string Phone { get; set; }
   

        public int Email { get; set; }
        public int Password { get; set; }
    }
}
