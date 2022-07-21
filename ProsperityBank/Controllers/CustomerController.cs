using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProsperityBank.Data;
using ProsperityBank.Filters;
using ProsperityBank.Models;
using ProsperityBank.Utilities;
using System.Threading.Tasks;

namespace ProsperityBank.Controllers
{

    [AuthorizeCustomer]
    public class CustomerController : Controller
    {

        private readonly ProsperityBankDBContext _context;

        //get the session customerID which is stored upon logged in 
        private int CustomerId => HttpContext.Session.GetInt32(nameof(CustomerController.CustomerId)).Value;

        public CustomerController(ProsperityBankDBContext context)
        {
            _context = context;
        }



        public async Task<IActionResult> Index()
        {
            var customer = await _context.Customers.Include(x => x.Accounts).
                FirstOrDefaultAsync(x => x.CustomerId == CustomerId);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        //GET method to find the customer id when we go into the deposit page
        public async Task<IActionResult> Deposit(int id) => View(await _context.Accounts.FindAsync(id));

        //post method for submission of deposit form/ page
        [HttpPost]
        public async Task<IActionResult> Deposit(int id, decimal amount, string comment)
        {
            //retrieve the account in the database
            var account = await _context.Accounts.Include(x => x.Transactions).
                FirstOrDefaultAsync(x => x.AccountNumber == id);

            //validation before submission 
            if (amount <= 0)
                ModelState.AddModelError(nameof(amount), "Amount must be positive.");
            if (amount.HasMoreThanTwoDecimalPlaces())
                ModelState.AddModelError(nameof(amount), "Amount cannot have more than 2 decimal places.");
            if (!ModelState.IsValid)
            {
                ViewBag.Amount = amount;
                return View(account);
            }

            // calling model
            account.Deposit(amount, comment);

            //updates database
            await _context.SaveChangesAsync();

            //return back to the /Customer
            return RedirectToAction(nameof(Index));
        }
    }
}
