using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using mvCommerce.Libraries.Login;
using mvCommerce.Models;
using System;

namespace mvCommerce.Libraries.Filter
{
    public class CollaboratorAuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        CollaboratorLogin _collaboratorLogin;
        private string _typeCollaboratorAuthorized;
        public CollaboratorAuthorizationAttribute(string typeCollaboratorAuthorized = "C")
        {
            _typeCollaboratorAuthorized = typeCollaboratorAuthorized;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _collaboratorLogin = (CollaboratorLogin)context.HttpContext.RequestServices.GetService(typeof(CollaboratorLogin));
            Collaborator collaborator = _collaboratorLogin.GetCollaborator();
            if (collaborator == null)
            {
                context.Result = new RedirectToActionResult("Login", "Home", null);
            }
            else
            {
                if(collaborator.Type == "C" && _typeCollaboratorAuthorized == "G")
                {
                    context.Result = new ContentResult() { Content = "Acesso negado!" };
                }
            }
        }
    }
}
