using ProsperityBank_API.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ProsperityBank_API.Model.DataManager;

namespace ProsperityBank_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerManager _repo;

        public CustomerController(CustomerManager repo)
        {
            _repo = repo;
        }

        /*
         * Returns single customer on given CustomerID
         */
        [HttpGet("{id}")]
        public Customer Get(int id)
        {
            return _repo.Get(id);
        }

        /*
         * Returns all customers
         */
        [HttpGet]
        public IEnumerable<Customer> GetAll()
        {
            return _repo.GetAll();
        }
    }
}
