using Microsoft.EntityFrameworkCore;

namespace FCoin.Models
{
    public class FDbContext : DbContext
    {
        public FDbContext(DbContextOptions<FDbContext> options) : base(options)
        {
        }

        // Add DbSet properties for your entities
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Validator> Validators { get; set; }


    }
}
