using AdminWeb.Attributes;
using ProsperityBank_ADMIN.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Threading.Tasks;
using ProsperityBank_ADMIN.Web.Helper;

namespace ProsperityBank_ADMIN.Controllers
{


    [AdminLogin]
    public class TransactionController : Controller
    {
        public async Task<IActionResult> Index(int? CustomerID, string StartDate, string EndDate)
        {
            CultureInfo enAU = new CultureInfo("en-AU");
            DateTime startDateVerified;


            if ((!DateTime.TryParseExact(StartDate, "dd/MM/yyyy hh:mm:ss tt", enAU,
                                 DateTimeStyles.None, out startDateVerified)) && (StartDate != null))
            {
                StartDate = null;
                ModelState.AddModelError(nameof(StartDate), "Date format invalid");
            }

            if ((!DateTime.TryParseExact(EndDate, "dd/MM/yyyy hh:mm:ss tt", enAU,
                                 DateTimeStyles.None, out startDateVerified)) && (EndDate != null))
            {
                EndDate = null;
                ModelState.AddModelError(nameof(EndDate), "Date format invalid");
            }




            var result = await AdminApi.InitializeClient().GetAsync("/transaction?" + $"customerID={CustomerID}&fromDate={StartDate}&toDate={EndDate}");

            if (!result.IsSuccessStatusCode)
            {
                throw new Exception("error to find transaction");
            }

            var data = result.Content.ReadAsStringAsync().Result;
            var transaction = JsonConvert.DeserializeObject<TransactionList>(data);

            return View(transaction);
        }
    }
}
