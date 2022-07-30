using ProsperityBank_API.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ProsperityBank_API.Model.DataManager;

namespace ProsperityBank_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly LoginManager _repo;

        public LoginController(LoginManager repo)
        {
            _repo = repo;
        }


        /*
         * Returns single Login on given LoginID
         */
        [HttpGet("{id}")]
        public Login Get(int id)
        {
            return _repo.Get(id);
        }


        /*
         * Returns all Logins
         */
        [HttpGet]
        public IEnumerable<Login> GetAll()
        {
            return _repo.GetAll();
        }

        /*
         * Calls Login.LockAccount on given LoginID
         */
        [HttpPut]
        [Route("LockAccount")]
        public bool LockAccount(int id)
        {
            return _repo.LockAccount(id);
        }


        /*
         * Calls Login.UnlockAccount on given LoginID
         */
        [HttpPut]
        [Route("UnLockAccount")]
        public bool UnLockAccount(int id)
        {
            return _repo.UnLockAccount(id);
        }
    }
}
