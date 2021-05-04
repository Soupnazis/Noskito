using System.Threading.Tasks;
using Noskito.World.Packet.Client.CharacterScreen;

namespace Noskito.World.Processor.CharacterScreen
{
    public class GameStartProcessor : PacketProcessor<GameStart>
    {
        protected override async Task Process(WorldSession session, GameStart packet)
        {
            
        }
    }
}