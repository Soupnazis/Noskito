using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Noskito.Database.Dto;

namespace Noskito.Database.Repository
{
    public class AccountRepository
    {
        private readonly DbContextFactory contextFactory;

        public AccountRepository(DbContextFactory contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public async Task<AccountDTO> GetAccountByName(string name)
        {
            using (var context = contextFactory.CreateContext())
            {
                var entity = await context.Accounts.FirstOrDefaultAsync(x => x.Username == name);
                if (entity == null)
                {
                    return default;
                }

                return new AccountDTO
                {
                    Id = entity.Id,
                    Username = entity.Username,
                    Password = entity.Password
                };
            }
        }
    }
}