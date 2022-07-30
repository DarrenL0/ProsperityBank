using ProsperityBank_API.Model;
using ProsperityBank_API.Model.DataManager;
using Microsoft.AspNetCore.Mvc;
using ProsperityBank_API.Models;

namespace ProsperityBank_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BillPayController : ControllerBase
    {
        private readonly BillPayManager _repo;

        public BillPayController(BillPayManager repo)
        {
            _repo = repo;
        }

        /*
         * Returns a single BillPay using BillPayID
         */
        [HttpGet("{id}")]
        public BillPay Get(int id)
        {
            return _repo.Get(id);
        }

        /*
         * Returns all BillPays if no paramater given
         * If a customerID is given, then all bill pays for that customer returned
         */
        [HttpGet]
        public BillPayViewModel GetAll(int? customerID)
        {
            return _repo.GetAll(customerID);
        }



        /*
         * Calls billPay.BlockBillPay on given BillPayID
         */
        [HttpPut]
        [Route("BlockBillPay")]
        public bool BlockBillPay(int id)
        {
            return _repo.BlockBillPay(id);
        }


        /*
        * Calls billPay.UnBlockBillPay on given BillPayID
        */
        [HttpPut]
        [Route("UnBlockBillPay")]
        public bool UnBlockBillPay(int id)
        {
            return _repo.UnBlockBillPay(id);
        }
    }
}
