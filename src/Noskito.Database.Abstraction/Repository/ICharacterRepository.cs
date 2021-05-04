using System.Collections.Generic;
using System.Threading.Tasks;
using Noskito.Database.Abstraction.Entity;

namespace Noskito.Database.Abstraction.Repository
{
    public interface ICharacterRepository
    {
        Task<IEnumerable<Character>> FindAll(long accountId);
        Task<bool> IsSlotTaken(long accountId, byte slot);
        Task<bool> IsNameTaken(string name);
        Task Create(Character character);
    }
}