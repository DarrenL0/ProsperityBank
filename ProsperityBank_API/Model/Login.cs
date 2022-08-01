using System;

#nullable disable

namespace ProsperityBank_API.Model
{
    public partial class Login
    {
        public string LoginId { get; set; }
        public int CustomerId { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreationTimeUtc { get; set; }
        public DateTime LockedUntil { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
