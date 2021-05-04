using System.Threading.Tasks;
using Noskito.World.Packet.Client.CharacterScreen;
using Noskito.World.Packet.Server.CharacterScreen;

namespace Noskito.World.Processor.CharacterScreen
{
    public class SelectProcessor : PacketProcessor<Select>
    {
        protected override async Task Process(WorldSession session, Select packet)
        {
            await session.SendPacket(new Ok());
        }
    }
}