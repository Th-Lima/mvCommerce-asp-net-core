using mvCommerce.Models;
using mvCommerce.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace mvCommerce.Libraries.Validation
{
    public class SingleEmailCollaboratorAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string email = (value as string).Trim();

            ICollaboratorRepository _collaboratorRepository = (ICollaboratorRepository)validationContext.GetService(typeof(ICollaboratorRepository));
            List<Collaborator> collaborators = _collaboratorRepository.GetCollaboratorPerEmail(email);

            Collaborator objCollaborator = (Collaborator)validationContext.ObjectInstance;

            if(collaborators.Count > 1)
            {
                return new ValidationResult("Este email já foi cadastrado por outro usuário");
            }
            if(collaborators.Count == 1 && objCollaborator.Id != collaborators[0].Id)
            {
                return new ValidationResult("Este email já foi cadastrado por outro usuário");
            }

            return ValidationResult.Success;
        }
    }
}
