using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using mvCommerce.Libraries.Login;
using mvCommerce.Models;
using System;

namespace mvCommerce.Libraries.Filter
{
    /*
     * Filters Types:
     * - Authorization
     * - Resource
     * - Action
     * - Execption
     * - Result
     */
    public class ClientAuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        ClientLogin _clientLogin;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
           _clientLogin = (ClientLogin)context.HttpContext.RequestServices.GetService(typeof(ClientLogin));
            Client client = _clientLogin.GetClient();
            if (client == null)
            {
               context.Result = new ContentResult() { Content = "Acesso negado!" };
            }
        }
    }
}
