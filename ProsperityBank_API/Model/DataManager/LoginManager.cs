using System.Collections.Generic;
using System.Linq;

namespace ProsperityBank_API.Model.DataManager
{
    public class LoginManager : IDataRepository<Login, int>
    {

        private readonly ProsperityBank_API_Context _context;

        public LoginManager(ProsperityBank_API_Context context)
        {
            _context = context;
        }


        public int Add(Login login)
        {
            _context.Logins.Add(login);
            _context.SaveChanges();

            return login.CustomerId;
        }

        public int Delete(int id)
        {
            _context.Logins.Remove(_context.Logins.Find(id));
            _context.SaveChanges();

            return id;
        }


        // uses the customer ID to get the customers login
        // Note that this is not the Login's login ID
        public Login Get(int id)
        {
            return _context.Logins.Where(x => x.CustomerId == id).FirstOrDefault();
        }

        public IEnumerable<Login> GetAll(int? loginID)
        {

            return _context.Logins.ToList();
        }

        public IEnumerable<Login> GetAll()
        {
            return _context.Logins.ToList();
        }

        public int Update(int id, Login login)
        {
            _context.Update(login);
            _context.SaveChanges();

            return id;
        }

        public bool LockAccount(int id)
        {
            var login = _context.Logins.Where(x => x.CustomerId == id).FirstOrDefault();

            if (login != null)
            {
                login.LockAccount(id);
                _context.SaveChanges();
                return true;
            }

            return false;
            
        }

        public bool UnLockAccount(int id)
        {
            var login = _context.Logins.Where(x => x.CustomerId == id).FirstOrDefault();

            if (login != null)
            {
                login.UnLockAccount(id);
                _context.SaveChanges();
                return true;
            }

            return false;

        }
    }
}
