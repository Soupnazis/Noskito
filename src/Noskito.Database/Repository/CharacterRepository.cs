using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Noskito.Database.Abstraction.Entity;
using Noskito.Database.Abstraction.Repository;
using Noskito.Database.Entity;

namespace Noskito.Database.Repository
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly DbContextFactory contextFactory;

        public CharacterRepository(DbContextFactory contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public async Task<IEnumerable<Character>> FindAll(long accountId)
        {
            using (var context = contextFactory.CreateContext())
            {
                IEnumerable<DbCharacter> entities = await context.Characters.Where(x => x.AccountId == accountId).ToListAsync();
                return entities.Select(x => new Character
                {
                    Id = x.Id,
                    AccountId = x.AccountId,
                    Name = x.Name,
                    Level = x.Level,
                    Slot = x.Slot,
                    Job = x.Job
                });
            }
        }

        public async Task<bool> IsSlotTaken(long accountId, byte slot)
        {
            using (var context = contextFactory.CreateContext())
            {
                var taken = await context.Characters.AnyAsync(x => x.AccountId == accountId && x.Slot == slot);
                return taken;
            }
        }

        public async Task<bool> IsNameTaken(string name)
        {
            using (var context = contextFactory.CreateContext())
            {
                var taken = await context.Characters.AnyAsync(x => x.Name.Equals(name));
                return taken;
            }
        }

        public async Task Create(Character character)
        {
            using (var context = contextFactory.CreateContext())
            {
                await context.Characters.AddAsync(new DbCharacter
                {
                    AccountId = character.AccountId,
                    Name = character.Name,
                    Slot = character.Slot
                });

                await context.SaveChangesAsync();
            }
        }
    }
}