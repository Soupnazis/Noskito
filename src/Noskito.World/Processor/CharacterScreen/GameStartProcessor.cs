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

            await session.SendTit(35);
            await session.SendFd(0, 1, 0, 1);
            await session.SendCInfo();
            await session.SendLev();
            await session.SendStat();

            await session.SendAt();
            await session.SendCMap();
        }
    }
}