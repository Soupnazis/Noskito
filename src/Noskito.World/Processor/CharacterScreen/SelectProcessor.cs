using System.Threading.Tasks;
using Noskito.Database.Repository;
using Noskito.World.Game;
using Noskito.World.Game.Entities;
using Noskito.World.Game.Maps;
using Noskito.World.Packet.Client.CharacterScreen;
using Noskito.World.Packet.Server.CharacterScreen;

namespace Noskito.World.Processor.CharacterScreen
{
    public class SelectProcessor : PacketProcessor<Select>
    {
        private readonly CharacterRepository characterRepository;
        private readonly MapManager mapManager;

        public SelectProcessor(MapManager mapManager, CharacterRepository characterRepository)
        {
            this.mapManager = mapManager;
            this.characterRepository = characterRepository;
        }

        protected override async Task Process(WorldSession session, Select packet)
        {
            if (session.Account is null)
            {
                await session.Disconnect();
                return;
            }

            var character = await characterRepository.GetCharacterInSlot(session.Account.Id, packet.Slot);
            if (character is null)
            {
                await session.Disconnect();
                return;
            }

            var map = await mapManager.GetMap(character.MapId);

            session.Character = new Character(session)
            {
                Id = character.Id,
                Name = character.Name,
                Job = character.Job,
                HairColor = character.HairColor,
                HairStyle = character.HairStyle,
                Level = character.Level,
                Position = new Position
                {
                    X = character.X,
                    Y = character.Y
                },
                Map = map,
                Hp = 300,
                Mp = 250,
                MaxHp = 300,
                MaxMp = 250
            };

            await session.SendPacket(new Ok());
        }
    }
}