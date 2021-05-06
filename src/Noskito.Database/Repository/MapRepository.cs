using System.Collections.Generic;
using System.Threading.Tasks;
using Noskito.Database.Dto;
using Noskito.Database.Entity;

namespace Noskito.Database.Repository
{
    public class MapRepository
    {
        private readonly DbContextFactory contextFactory;
        private readonly Mapper<DbMap, MapDTO> mapper;

        public MapRepository(DbContextFactory contextFactory, Mapper<DbMap, MapDTO> mapper)
        {
            this.contextFactory = contextFactory;
            this.mapper = mapper;
        }

        public async Task<MapDTO> Find(int mapId)
        {
            using (var context = contextFactory.CreateContext())
            {
                var entity = await context.Maps.FindAsync(mapId);
                if (entity == null)
                {
                    return default;
                }

                return mapper.Map(entity);
            }
        }

        public async Task SaveAll(IEnumerable<MapDTO> maps)
        {
            using (var context = contextFactory.CreateContext())
            {
                await context.Maps.AddRangeAsync(mapper.Map(maps));
                await context.SaveChangesAsync();
            }
        }
    }
}