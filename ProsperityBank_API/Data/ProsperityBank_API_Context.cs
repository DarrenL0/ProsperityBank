using Microsoft.EntityFrameworkCore;


#nullable disable

namespace ProsperityBank_API.Model
{
    public partial class ProsperityBank_API_Context : DbContext
    {
        public ProsperityBank_API_Context()
        {
        }

        public ProsperityBank_API_Context(DbContextOptions<ProsperityBank_API_Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<BillPay> BillPays { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<Payee> Payees { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // warning supressed as we are sharing connection string into github

#pragma warning disable CS1030 // #warning directive
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=LAPTOP-QP1JOLTK; Database=ProsperityBank; Trusted_Connection=True; MultipleActiveResultSets=True");
#pragma warning restore CS1030 // #warning directive
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.AccountNumber);

                entity.HasIndex(e => e.CustomerId, "IX_Accounts_CustomerId");

                entity.Property(e => e.AccountNumber).ValueGeneratedNever();

                entity.Property(e => e.Balance).HasColumnType("money");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerId");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.CustomerId);
            });

            modelBuilder.Entity<BillPay>(entity =>
            {
                entity.HasIndex(e => e.AccountNumber, "IX_BillPays_AccountNumber");

                entity.HasIndex(e => e.PayeeId, "IX_BillPays_PayeeID");

                entity.Property(e => e.BillPayId).HasColumnName("BillPayID");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.Blocked)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.PayeeId).HasColumnName("PayeeID");

                entity.HasOne(d => d.AccountNumberNavigation)
                    .WithMany(p => p.BillPays)
                    .HasForeignKey(d => d.AccountNumber);

                entity.HasOne(d => d.Payee)
                    .WithMany(p => p.BillPays)
                    .HasForeignKey(d => d.PayeeId);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.CustomerId)
                    .ValueGeneratedNever()
                    .HasColumnName("CustomerId");

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.City).HasMaxLength(40);

                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.Postcode).HasMaxLength(4);

                entity.Property(e => e.Tfn)
                    .HasMaxLength(11)
                    .HasColumnName("TFN");
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.HasIndex(e => e.CustomerId, "IX_Logins_CustomerId");

                entity.Property(e => e.LoginId)
                    .HasMaxLength(8)
                    .HasColumnName("LoginID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerId");

                entity.Property(e => e.LockedUntil).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Logins)
                    .HasForeignKey(d => d.CustomerId);
            });

            modelBuilder.Entity<Payee>(entity =>
            {
                entity.HasIndex(e => e.BillPayId, "IX_Payees_BillPayID");

                entity.Property(e => e.PayeeId).HasColumnName("PayeeID");

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.BillPayId).HasColumnName("BillPayID");

                entity.Property(e => e.City).HasMaxLength(40);

                entity.Property(e => e.PayeeName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.PostCode).HasMaxLength(4);

                entity.HasOne(d => d.BillPay)
                    .WithMany(p => p.Payees)
                    .HasForeignKey(d => d.BillPayId);
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasIndex(e => e.AccountNumber, "IX_Transactions_AccountNumber");

                entity.HasIndex(e => e.DestinationAccountNumber, "IX_Transactions_DestinationAccountNumber");

                entity.Property(e => e.TransactionId).HasColumnName("TransactionID");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.Comment).HasMaxLength(255);

                entity.HasOne(d => d.AccountNumberNavigation)
                    .WithMany(p => p.TransactionAccountNumberNavigations)
                    .HasForeignKey(d => d.AccountNumber);

                entity.HasOne(d => d.DestinationAccountNumberNavigation)
                    .WithMany(p => p.TransactionDestinationAccountNumberNavigations)
                    .HasForeignKey(d => d.DestinationAccountNumber);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
