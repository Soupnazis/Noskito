using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Noskito.Database.Entity;

namespace Noskito.Database
{
    public class NoskitoContext : DbContext
    {
        public DbSet<DbAccount> Accounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql($"Host=localhost;Database=noskito;Username=noskito;Password=noskito");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}