using Microsoft.EntityFrameworkCore;
using Noskito.Database.Entity;

namespace Noskito.Database
{
    public class NoskitoContext : DbContext
    {
        public DbSet<DbAccount> Accounts { get; set; }
        public DbSet<DbCharacter> Characters { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=noskito;Username=noskito;Password=noskito");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DbAccount>()
                .HasMany(x => x.Characters)
                .WithOne(x => x.Account)
                .HasForeignKey(x => x.AccountId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}