using API.PaymentTransactions.Shared;
using Microsoft.EntityFrameworkCore;

namespace API.PaymentTransactions.Data
{
    public class APIPaymentTransactionsContext : DbContext
    {
        public APIPaymentTransactionsContext(DbContextOptions options) : base(options) { }

        DbSet<Count> Counts { get; set; } 
        DbSet<Field> Fields { get; set; }
        DbSet<Fields> ListOfFields { get; set; }
        DbSet<Mount> Mounts { get; set; }
        DbSet<Payer> Payers { get; set; }
        DbSet<Payment> Payments { get; set; }
        DbSet<Receipt> Receipts { get; set; }
        DbSet<RequesByCount> requesByCounts { get; set; }
        DbSet<Status> statuses { get; set; }    
        


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer();
        }
    }
}
