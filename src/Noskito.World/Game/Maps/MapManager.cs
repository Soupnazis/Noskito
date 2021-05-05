namespace Noskito.World.Game.Maps
{
    public class MapManager
    {
        public Map GetMap(int mapId)
        {
            return new()
            {
                Id = mapId,
                Name = "Undefined"
            };
        }
    }
}