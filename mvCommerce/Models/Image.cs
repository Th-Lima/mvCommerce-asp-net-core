using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace mvCommerce.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Path { get; set; }

        //Database
        public int ProductId { get; set; }

        //POO
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}
