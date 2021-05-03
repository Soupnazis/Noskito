using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Noskito.Database.Abstraction.Entity;
using Noskito.Database.Abstraction.Repository;

namespace Noskito.Database.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DbContextFactory contextFactory;

        public AccountRepository(DbContextFactory contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public async Task<Account> GetAccountByName(string name)
        {
            using (var context = contextFactory.CreateContext())
            {
                var entity = await context.Accounts.FirstOrDefaultAsync(x => x.Username == name);
                if (entity == null)
                {
                    return default;
                }

                return new Account
                {
                    Username = entity.Username,
                    Password = entity.Password
                };
            }
        }
    }
}