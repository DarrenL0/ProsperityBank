using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProsperityBank.Data;
using ProsperityBank.Models;
using SimpleHashing;
using System;
using System.Threading.Tasks;

namespace ProsperityBank.Controllers
{
    public class LoginController : Controller
    {

        //connection string
        private readonly ProsperityBankDBContext _context;

        public LoginController(ProsperityBankDBContext context)
        {
            _context = context;
        }

        //goes into the view and finds a Login.cshtml
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string loginID, string password)
        {
            //find the login ID in the database 
            var login = await _context.Logins.FindAsync(loginID);

            //adding dail condition if account has been suspended beyond the present datetime
            if (login != null && DateTime.Compare(DateTime.UtcNow, login.LockedUntil) < 0)
            {
                ModelState.AddModelError("LoginFailed", "Account has been locked until " + login.LockedUntil.ToLocalTime());
                return View(new Login { LoginID = loginID });
            }
            
            if ((login == null) || (password == null) || !PBKDF2.Verify(login.PasswordHash, password))
            {
                ModelState.AddModelError("LoginFailed", "Login failed, please try again.");
                return View(new Login { LoginID = loginID });
            }

            //Login customer 
            //this here mean login has worked and now it store the infomation into the session
            //which is storing the customerID and Name
            HttpContext.Session.SetInt32(nameof(Customer.CustomerId), login.CustomerID);
            HttpContext.Session.SetString(nameof(Customer.CustomerName), login.Customer.CustomerName);

            //redirects to the Index method in the Customer Controller
            return RedirectToAction("Index", "Customer");
        }

        [Route("LogoutNow")]

        public IActionResult Logout()
        {
            //Logout customer 
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }


    }
}
