using Noskito.Enum;
using Noskito.World.Game.Maps;

namespace Noskito.World.Game.Entities
{
    public abstract class Entity
    {
        public long Id { get; init; }
        public EntityType EntityType { get; init; }
        public string Name { get; set; }

        public Map Map { get; set; }
        public Position Position { get; set; }
    }
}