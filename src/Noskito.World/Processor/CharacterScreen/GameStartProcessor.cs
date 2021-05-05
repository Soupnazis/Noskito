using System.Threading.Tasks;
using Noskito.World.Packet.Client.CharacterScreen;
using Noskito.World.Processor.Extension;

namespace Noskito.World.Processor.CharacterScreen
{
    public class GameStartProcessor : PacketProcessor<GameStart>
    {
        protected override async Task Process(WorldSession session, GameStart packet)
        {
            var character = session.Character;

            await session.SendTit(35, character.Name);
            await session.SendFd(0, 1, 0, 1);
            await session.SendCInfo(character);
            await session.SendLev(character);

            await session.SendAt(character);
            await session.SendCMap(character.Map);
        }
    }
}