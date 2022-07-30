using ProsperityBank_API.Model;
using Microsoft.AspNetCore.Mvc;
using ProsperityBank_API.Model.DataManager;

namespace ProsperityBank_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly TransactionManager _repo;

        public TransactionController(TransactionManager repo)
        {
            _repo = repo;
        }

        /*
         * Returns single Transaction on given Transaction
         */
        [HttpGet("{id}")]
        public Transaction Get(int id)
        {
            return _repo.Get(id);
        }

        /*
         * returns All Transaction if no paramaters supplied
         * If customerID is supplied then only Transaction belonging to accounts
         * belonging to given customerID are returned
         * 
         * if dates are given then only Transactions from-to given dates are returned
         */
        [HttpGet]
        public TransactionViewModel GetAll(int? customerID, string fromDate, string toDate)
        {
            return _repo.GetAll(customerID, fromDate, toDate);
        }


    }
}
