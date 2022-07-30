using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;


namespace AdminWeb.Attributes
{
    public class AdminLoginAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var admin = context.HttpContext.Session.GetString("admin");
            if (admin == null)
            {
                context.Result = new RedirectToActionResult("Login", "AdminLogin", null);
            }
        }
    }
}
