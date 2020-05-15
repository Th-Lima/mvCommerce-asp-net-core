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

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _collaboratorLogin = (CollaboratorLogin)context.HttpContext.RequestServices.GetService(typeof(CollaboratorLogin));
            Collaborator client = _collaboratorLogin.GetCollaborator();
            if (client == null)
            {
                context.Result = new ContentResult() { Content = "Acesso negado!" };
            }
        }
    }
}
