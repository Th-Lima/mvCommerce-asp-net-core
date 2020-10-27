using mvCommerce.Libraries.Lang;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mvCommerce.Models
{
    public class DeliveryAddress
    {
        //PK
        public int Id { get; set; }

        [MinLength(3, ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E002")]
        public string Name { get; set; }
       
        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E001")]
        [Display(Name = "Estado")]
        public string State { get; set; }

        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E001")]
        [Display(Name = "Cidade")]
        public string City { get; set; }

        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E001")]
        [Display(Name = "Bairro")]
        public string Neighborhood { get; set; }

        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E001")]
        [Display(Name = "Endereço")]
        public string Address { get; set; }

        [Display(Name = "Complemento")]
        public string Complement { get; set; }

        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E001")]
        [Display(Name = "Número")]
        public string Number { get; set; }

        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E001")]
        [Display(Name = "CEP")]
        [MinLength(10, ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E002")]
        [MaxLength(10, ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E003")]
        public string Zipcode { get; set; }

        [ForeignKey("Client")]
        public int? ClientId { get; set; }

        public virtual Client Client { get; set; }
    }
}
