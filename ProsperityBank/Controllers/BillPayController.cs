using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProsperityBank.Data;
using ProsperityBank.Models;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProsperityBank.Utilities;
using X.PagedList;

namespace ProsperityBank.Controllers
{
    public class BillPayController : Controller
    {

        private readonly ProsperityBankDBContext _context;

        private int CustomerID => HttpContext.Session.GetInt32(nameof(Customer.CustomerId)).Value;

        public BillPayController(ProsperityBankDBContext context)
        {
            _context = context;
        }

        //Method to retrieve billpay assocaited to the logged in customer's account
        public async Task<IActionResult> Index()
        {
            var customer = await _context.Customers.Include(x => x.Accounts).
                FirstOrDefaultAsync(x => x.CustomerId == CustomerID);

            var payeeList = (from bill in _context.BillPays
                             join acc in _context.Accounts
                             on bill.AccountNumber equals acc.AccountNumber
                             where acc.CustomerID == CustomerID
                             select bill).Include(x => x.Payee);

            return View(await payeeList.ToListAsync());
        }

        //Method to populate the drop down box for user to create a bill pay
        public IActionResult Create()
        {
            List<PeriodType> periods = Enum.GetValues(typeof(PeriodType)).Cast<PeriodType>().ToList();

            ViewBag.Periods = new SelectList(periods);
            ViewData["AccountNumber"] = new SelectList(_context.Accounts.Where(x => x.CustomerID == CustomerID).ToList(), "AccountNumber", "AccountNumber");
            ViewData["PayeeID"] = new SelectList(_context.Payees.ToList(), "PayeeID", "PayeeName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(int accountNumber, int payeeID, decimal amount, DateTime scheduleDate, PeriodType periodType)
        {
            var billPay = new BillPay();
            billPay.AccountNumber = accountNumber;
            billPay.PayeeID = payeeID;
            billPay.Amount = amount;
            billPay.ScheduleDate = scheduleDate;
            billPay.PeriodType = periodType;
            billPay.ModifyDate = DateTime.UtcNow;


            //validation
            if (MiscellaneousExtensionUtilities.HasMoreThanTwoDecimalPlaces(amount))
            {
                ModelState.AddModelError(nameof(amount), "Amount cannot have more than 2 decimal places.");
            }
            if (amount <= 0)
                ModelState.AddModelError(nameof(amount), "Amount must be positive.");
            if (ModelState.IsValid)
            {
                //validation passed create bill pay and insert into database
                billPay.ScheduleDate = TimeZoneInfo.ConvertTimeToUtc(billPay.ScheduleDate);
                _context.Add(billPay);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            //validation fail go back to show error 
            List<PeriodType> periods = Enum.GetValues(typeof(PeriodType)).Cast<PeriodType>().ToList();
            ViewBag.Periods = new SelectList(periods);
            ViewData["AccountNumber"] = new SelectList(_context.Accounts, "AccountNumber", "AccountNumber", billPay.AccountNumber);
            ViewData["PayeeID"] = new SelectList(_context.Payees.ToList(), "PayeeID", "PayeeName");

            return View();
        }

        public async Task<IActionResult> Modify(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billPay = await _context.BillPays.FindAsync(id);
            TimeZoneInfo format = TimeZoneInfo.FindSystemTimeZoneById("AUS Eastern Standard Time");
            billPay.ScheduleDate = TimeZoneInfo.ConvertTimeFromUtc(billPay.ScheduleDate, format);

            if (billPay == null)
            {
                return NotFound();
            }
            List<PeriodType> periods = Enum.GetValues(typeof(PeriodType)).Cast<PeriodType>().ToList();

            ViewBag.Periods = new SelectList(periods);
            ViewData["AccountNumber"] = new SelectList(_context.Accounts.Where(x => x.CustomerID == CustomerID), "AccountNumber", "AccountNumber", billPay.AccountNumber);
            ViewData["PayeeID"] = new SelectList(_context.Payees, "PayeeID", "PayeeName", billPay.PayeeID);
            return View(billPay);
        }

        [HttpPost]
        public async Task<IActionResult> Modify(int id, int accountNumber, int payeeID, decimal amount, DateTime scheduleDate, PeriodType periodType)
        {
            var billPay = new BillPay();
            billPay.BillPayID = id;
            billPay.AccountNumber = accountNumber;
            billPay.PayeeID = payeeID;
            billPay.Amount = amount;
            billPay.ScheduleDate = scheduleDate;
            billPay.PeriodType = periodType;
            billPay.ModifyDate = DateTime.UtcNow;

            //validation
            if (amount <= 0)
                ModelState.AddModelError(nameof(amount), "Amount must be positive.");
            if (MiscellaneousExtensionUtilities.HasMoreThanTwoDecimalPlaces(billPay.Amount))
            {
                ModelState.AddModelError(nameof(billPay.Amount), "Amount cannot have more than 2 decimal places.");
            }
            if (ModelState.IsValid)
            {
                billPay.ScheduleDate = TimeZoneInfo.ConvertTimeToUtc(billPay.ScheduleDate);
                _context.Add(billPay);
                _context.Update(billPay);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            //if model is not valid
            List<PeriodType> periods = Enum.GetValues(typeof(PeriodType)).Cast<PeriodType>().ToList();
            ViewBag.Periods = new SelectList(periods);
            ViewData["AccountNumber"] = new SelectList(_context.Accounts, "AccountNumber", "AccountNumber", billPay.AccountNumber);
            ViewData["PayeeID"] = new SelectList(_context.Payees.ToList(), "PayeeID", "PayeeName");
            return View(billPay);

        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billPay = await _context.BillPays.FindAsync(id);

            TimeZoneInfo format = TimeZoneInfo.FindSystemTimeZoneById("AUS Eastern Standard Time");
            billPay.ScheduleDate = TimeZoneInfo.ConvertTimeFromUtc(billPay.ScheduleDate, format);

            if (billPay == null)
            {
                return NotFound();
            }

            return View(billPay);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var billPay = await _context.BillPays.FindAsync(id);
            _context.BillPays.Remove(billPay);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Statement(int? page = 1)
        {
            // maximum of 4 per page.
            const int pageSize = 4;

            var pagedList = await (from t in _context.Transactions
                                   join a in _context.Accounts
                                   on t.AccountNumber equals a.AccountNumber
                                   where a.CustomerID == CustomerID
                                   where t.TransactionType == TransactionType.BillPay
                                   select t).ToPagedListAsync(page, pageSize);

            return View(pagedList);
        }

    }
}
