using mvCommerce.Libraries.Lang;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mvCommerce.Models.ProductAggregator
{
    public class Product
    {
        public int Id { get; set; }
        
        [JsonIgnore]
        [Display(Name = "Nome")]
        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E001")]
        public string Name { get; set; }
        
        [Display(Name = "Preço")]
        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E001")]
        public decimal Price { get; set; }

        [JsonIgnore]
        [Display(Name = "Descrição")]
        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E001")]
        [MinLength(15, ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E002")]
        [MaxLength(240)]
        public string Description { get; set; }

        [JsonIgnore]
        [Display(Name = "Quantidade")]
        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E001")]
        [Range(0, 1000000, ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E006")]
        public int Amount { get; set; }

        //Freight - Correios
        [JsonIgnore]
        [Display(Name = "Peso")]
        [Range(0.001, 30, ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E006")]
        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E001")]
        public double Weight { get; set; }

        [JsonIgnore]
        [Display(Name = "Altura")]
        [Range(2, 105, ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E006")]
        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E001")]
        public int Height { get; set; }

        [JsonIgnore]
        [Display(Name = "Largura")]
        [Range(11, 105, ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E006")]
        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E001")]
        public int Width { get; set; }

        [JsonIgnore]
        [Display(Name = "Comprimento")]
        [Range(16, 105, ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E006")]
        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E001")]
        public int Length { get; set; }

        //Database
        [JsonIgnore]
        [Display(Name = "Categoria")]
        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E001")]
        public int CategoryId { get; set; }

        //POO
        [JsonIgnore]
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        [JsonIgnore]
        public virtual ICollection<Image> Images { get; set; }

    }
}
