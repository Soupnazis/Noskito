using System.Threading.Tasks;
using Noskito.Common.Logging;
using Noskito.World.Abstraction.Network;
using Noskito.World.Packet.Client;

namespace Noskito.World.Processor
{
    public class UnresolvedProcessor : PacketProcessor<UnresolvedPacket>
    {
        public UnresolvedProcessor(ILogger logger)
        {
            this.logger = logger;
        }

        private readonly ILogger logger;
        
        
        
        protected override Task Process(IWorldClient client, UnresolvedPacket packet)
        {
            if (client.EncryptionKey == 0)
            {
                client.EncryptionKey = int.Parse(packet.Header);
            }
            
            return Task.CompletedTask;
        }
    }
}