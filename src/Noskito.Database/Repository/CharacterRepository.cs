using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Noskito.Database.Dto;
using Noskito.Database.Entity;

namespace Noskito.Database.Repository
{
    public class CharacterRepository
    {
        private readonly DbContextFactory contextFactory;

        public CharacterRepository(DbContextFactory contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public async Task<IEnumerable<CharacterDTO>> FindAll(long accountId)
        {
            using (var context = contextFactory.CreateContext())
            {
                IEnumerable<DbCharacter> entities = await context.Characters.Where(x => x.AccountId == accountId).ToListAsync();
                return entities.Select(x => new CharacterDTO
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

        public async Task Create(CharacterDTO character)
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