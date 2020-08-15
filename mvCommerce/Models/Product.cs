using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace mvCommerce.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }

        //Freight - Correios
        public double Weight { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int Length { get; set; }

        //Database
        public int CategoryId { get; set; }

        //POO
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public virtual ICollection<Image> Images { get; set; }

    }
}
