using API.PaymentTransactions.Shared;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.PaymentTransactions.Data
{
    public class APIPaymentTransactionsContext : IdentityDbContext
    {
        public APIPaymentTransactionsContext(DbContextOptions options) : base(options) { }

        public DbSet<Count> Counts { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<Fields> ListOfFields { get; set; }
        public DbSet<Mount> Mounts { get; set; }
        public DbSet<Payer> Payers { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<RequesByCount> requesByCounts { get; set; }
        public DbSet<Status> statuses { get; set; }    
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer();
        }
    }
}
