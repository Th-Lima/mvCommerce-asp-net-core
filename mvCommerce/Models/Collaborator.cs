using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mvCommerce.Models
{
    public class Collaborator
    {
        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E001")]
        [MinLength(4, ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E002")]
        [Display(Name="Código")]
        public int Id { get; set; }

        [Display(Name="Nome")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E001")]
        [EmailAddress(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E004")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E001")]
        [MinLength(6, ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E002")]
        [Display(Name="Senha")]
        public string Password { get; set; }

        [NotMapped]
        public string ConfirmationPassword { get; set; }

        /*
         *  Comum or Manager
         *  c -> Comum
         *  m -> Manager
         */
        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E001")]
        [Display(Name="Tipo")]
        public string Type { get; set; }
    }
}
