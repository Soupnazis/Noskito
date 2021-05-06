using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Noskito.Common.Extension;
using Noskito.Database.Dto;
using Noskito.Database.Repository;

namespace Noskito.World.Game.Maps
{
    public class MapManager
    {
        private readonly MapRepository mapRepository;

        private readonly Dictionary<int, Map> maps = new();
        
        public MapManager(MapRepository mapRepository)
        {
            this.mapRepository = mapRepository;
        }

        public async Task<Map> GetMap(int mapId)
        {
            var map = maps.GetValueOrDefault(mapId);
            if (map == null)
            {
                var dto = await mapRepository.Find(mapId);
                if (dto == null)
                {
                    return default;
                }

                map = new Map
                {
                    Id = dto.Id,
                    Grid = dto.Grid,
                    Width = BitConverter.ToInt16(dto.Grid.Slice(0, 2)),
                    Height = BitConverter.ToInt16(dto.Grid.Slice(2, 2))
                };
            }

            return map;
        }
    }
}