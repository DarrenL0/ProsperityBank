using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProsperityBank.Models;

namespace ProsperityBank.Filters
{
    //inherits from attribute, which you can use the square bracket syntax
    public class AuthorizeCustomerAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //validate if a user is logged in 
            var customerID = context.HttpContext.Session.GetInt32(nameof(Customer.CustomerId));
            if (!customerID.HasValue)
            {
                //if not logged in then will be redirect to login page
                context.Result = new RedirectToActionResult("Login", "Login", null);
            }
        }
    }
}
