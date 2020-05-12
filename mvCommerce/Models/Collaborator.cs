using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvCommerce.Models
{
    public class Collaborator
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        /*
         *  Comum or Manager
         *  c -> Comum
         *  m -> Manager
         */
        public string Type { get; set; }
    }
}
