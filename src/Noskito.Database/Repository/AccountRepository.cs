using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Noskito.Database.Dto;
using Noskito.Database.Entity;

namespace Noskito.Database.Repository
{
    public class AccountRepository
    {
        private readonly DbContextFactory contextFactory;
        private readonly Mapper<DbAccount, AccountDTO> mapper;

        public AccountRepository(DbContextFactory contextFactory, Mapper<DbAccount, AccountDTO> mapper)
        {
            this.contextFactory = contextFactory;
            this.mapper = mapper;
        }

        public async Task<AccountDTO> GetAccountByName(string name)
        {
            using (var context = contextFactory.CreateContext())
            {
                var entity = await context.Accounts.FirstOrDefaultAsync(x => x.Username == name);
                if (entity is null)
                {
                    return default;
                }

                return mapper.Map(entity);
            }
        }
    }
}