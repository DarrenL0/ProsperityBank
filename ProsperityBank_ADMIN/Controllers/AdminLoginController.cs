using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProsperityBank_ADMIN.Controllers
{
    public class AdminLoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string loginID, string password)
        {
            if ((loginID == null) || !(loginID.Equals("admin")))
            {
                ModelState.AddModelError("LoginFailed", "Login failed please try again");
                return View();
            }

            if ((password == null) || !(password.Equals("admin")))
            {
                ModelState.AddModelError("LoginFailed", "Login failed please try again");
                return View();
            }

            HttpContext.Session.SetString("admin", loginID);
            HttpContext.Session.SetString("admin_name", loginID);

            return RedirectToAction("Index", "User");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "AdminLogin");
        }
    }
}
