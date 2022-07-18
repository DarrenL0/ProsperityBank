using Microsoft.EntityFrameworkCore;
using ProsperityBank.Models;

namespace Rental.Data
{
    public class ProsperityBankDBContext :DbContext
    {
        public ProsperityBankDBContext(DbContextOptions<ProsperityBankDBContext> options) : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }
    }
}
