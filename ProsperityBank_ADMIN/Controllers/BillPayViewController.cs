using AdminWeb.Attributes;
using ProsperityBank_ADMIN.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using ProsperityBank_ADMIN.Web.Helper;

namespace ProsperityBank_ADMIN.Controllers
{
    [AdminLogin]
    public class BillPayViewController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        private HttpClient Client => _clientFactory.CreateClient("api");

        public BillPayViewController(IHttpClientFactory clientFactory) => _clientFactory = clientFactory;


        public async Task<IActionResult> Index(int? CustomerID)
        {

            var result = await AdminApi.InitializeClient().GetAsync("/billpay?" + $"customerID={CustomerID}");

            if (!result.IsSuccessStatusCode)
            {
                throw new Exception("Error in finding BillPay");
            }

            var data = result.Content.ReadAsStringAsync().Result;
            var billpays = JsonConvert.DeserializeObject<BillPayList>(data);

            return View(billpays);
        }

        [HttpGet]
        public async Task<IActionResult> BlockBillPay(int id)
        {
            var response = await Client.GetAsync("BillPay/" + id);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception();
            }

            var data = response.Content.ReadAsStringAsync().Result;
            var billPay = JsonConvert.DeserializeObject<BillPayDTO>(data);

            return View(billPay);
        }



        [HttpPost]
        public async Task<IActionResult> BlockBillPayPost(int id)
        {
            var response = await Client.PutAsync("BillPay/BlockBillPay?id=" + id, null);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception();
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> UnBlockBillPay(int id)
        {
            var response = await Client.GetAsync("BillPay/" + id);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception();
            }

            var data = response.Content.ReadAsStringAsync().Result;
            var billPay = JsonConvert.DeserializeObject<BillPayDTO>(data);

            return View(billPay);
        }

        [HttpPost]
        public async Task<IActionResult> UnBlockBillPayPost(int id)
        {
            // API only needs customer ID int to suspend account
            var response = await Client.PutAsync("BillPay/UnBlockBillPay?id=" + id, null);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception();
            }

            //return back to billpays
            return RedirectToAction(nameof(Index));
        }
    }
}
