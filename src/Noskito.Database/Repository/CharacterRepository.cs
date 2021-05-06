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
        private readonly Mapper<DbCharacter, CharacterDTO> mapper;

        public CharacterRepository(DbContextFactory contextFactory, Mapper<DbCharacter, CharacterDTO> mapper)
        {
            this.contextFactory = contextFactory;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<CharacterDTO>> FindAll(long accountId)
        {
            using (var context = contextFactory.CreateContext())
            {
                IEnumerable<DbCharacter> entities =
                    await context.Characters.Where(x => x.AccountId == accountId).ToListAsync();
                if (entities is null) return default;

                return mapper.Map(entities);
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

        public async Task Save(CharacterDTO character)
        {
            using (var context = contextFactory.CreateContext())
            {
                await context.Characters.AddAsync(mapper.Map(character));
                await context.SaveChangesAsync();
            }
        }

        public async Task<CharacterDTO> GetCharacterInSlot(long accountId, byte slot)
        {
            using (var context = contextFactory.CreateContext())
            {
                var entity =
                    await context.Characters.FirstOrDefaultAsync(x => x.AccountId == accountId && x.Slot == slot);
                if (entity is null) return default;

                return mapper.Map(entity);
            }
        }
    }
}