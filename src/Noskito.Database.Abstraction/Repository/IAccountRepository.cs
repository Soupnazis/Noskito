using System.Threading.Tasks;
using Noskito.Database.Abstraction.Entity;

namespace Noskito.Database.Abstraction.Repository
{
    public interface IAccountRepository
    {
        Task<Account> GetAccountByName(string name);
    }
}