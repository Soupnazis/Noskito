
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Noskito.Common.Logging;
using Noskito.Database.Dto;
using Noskito.Database.Repository;
using TextReader = Noskito.Toolkit.Parsing.Reader.TextReader;

namespace Noskito.Toolkit.Parsing
{
    public class MapParser : IParser
    {
        private readonly ILogger logger;
        private readonly MapRepository mapRepository;

        public MapParser(ILogger logger, MapRepository mapRepository)
        {
            this.logger = logger;
            this.mapRepository = mapRepository;
        }

        public async Task Parse(DirectoryInfo directory)
        {
            logger.Information("Parsing maps");
            
            var mapDirectory = directory.GetDirectories().FirstOrDefault(x => x.Name == "Maps");
            if (mapDirectory == null)
            {
                logger.Warning("Missing Maps directory, skipping map parsing");
                return;
            }

            var mapFiles = mapDirectory.EnumerateFiles("*.bin");
            if (!mapFiles.Any())
            {
                logger.Warning("Can't found any map file in Maps directory, skipping map parsing");
                return;
            }

            var mapIdData = directory.GetFiles().FirstOrDefault(x => x.Name == "MapIDData.dat");
            if (mapIdData == null)
            {
                logger.Warning("Can't found MapIDData.dat, skipping map parsing");
                return;
            }
            
            var content = TextReader.FromFile(mapIdData)
                .SkipLines(x => x.StartsWith("DATA"))
                .SkipCommentedLines("#")
                .SkipEmptyLines()
                .SplitLineContent(' ')
                .TrimLines()
                .GetContent();

            var mapNameKeys = new Dictionary<int, string>();
            foreach (var line in content.Lines)
            {
                var firstMapId = line.GetValue<int>(0);
                var secondMapId = line.GetValue<int>(1);
                var nameKey = line.GetValue(4);

                for (var i = firstMapId; i <= secondMapId; i++)
                {
                    mapNameKeys[i] = nameKey;
                }
            }
            
            var maps = new List<MapDTO>();
            foreach (var mapFile in mapFiles)
            {
                var mapId = int.Parse(Path.GetFileNameWithoutExtension(mapFile.Name));
                maps.Add(new MapDTO
                {
                    Id = int.Parse(Path.GetFileNameWithoutExtension(mapFile.Name)),
                    Name = mapNameKeys.GetValueOrDefault(mapId, "UNDEFINED"),
                    Grid = await File.ReadAllBytesAsync(mapFile.FullName)
                });
            }

            await mapRepository.SaveAll(maps);
            
            logger.Information($"Saved {maps.Count} maps into database");
        }
    }
}