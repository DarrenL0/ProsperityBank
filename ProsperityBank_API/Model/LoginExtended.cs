using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


#nullable disable

namespace ProsperityBank_API.Model
{
    // this class is not working for some reason!!
    public partial class Login
    {

        // locks the account for 1 minute
        public void LockAccount(int id)
        {
            LockedUntil = DateTime.UtcNow.AddMinutes(1);
        }

        // sets the lock time to current time to unlock account whilst 
        // storing relevant lock Datetime in D/B
        public void UnLockAccount(int id)
        {
            LockedUntil = DateTime.UtcNow;
        }
    }
}
