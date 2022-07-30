using AdminWeb.Attributes;
using ProsperityBank_ADMIN.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProsperityBank_ADMIN.Controllers
{
    [AdminLogin]
    public class UserController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        private HttpClient Client => _clientFactory.CreateClient("api");

        public UserController(IHttpClientFactory clientFactory) => _clientFactory = clientFactory;
        //GET: User/Index
        public async Task<IActionResult> Index()
        {

            var response = await Client.GetAsync("customer");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception();
            }

            var data = response.Content.ReadAsStringAsync().Result;
            var list = JsonConvert.DeserializeObject<List<CustomerDTO>>(data);

            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> SuspendCustomer(int id)
        {
            var response = await Client.GetAsync("customer/" + id);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception();
            }

            var data = response.Content.ReadAsStringAsync().Result;
            var customer = JsonConvert.DeserializeObject<CustomerDTO>(data);

            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> SuspendCustomerPost(int id)
        {
            // API only needs customer ID int to suspend account
            var response = await Client.PutAsync("Login/LockAccount?id=" + id, null);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception();
            }

            //return back to the /Customer
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> UnLockCustomer(int id)
        {
            var response = await Client.GetAsync("customer/" + id);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception();
            }

            var data = response.Content.ReadAsStringAsync().Result;
            var customer = JsonConvert.DeserializeObject<CustomerDTO>(data);

            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> UnLockCustomerPost(int id)
        {
            // API only needs customer ID int to suspend account
            var response = await Client.PutAsync("Login/UnLockAccount?id=" + id, null);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception();
            }

            //return back to the /Customer
            return RedirectToAction(nameof(Index));
        }
    }
}
