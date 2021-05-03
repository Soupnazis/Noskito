using System.Collections.Generic;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using Noskito.Common.Logging;
using Noskito.Packet;
using Noskito.Packet.Server;

namespace Noskito.Login.Network.Pipeline
{
    public class Serializer : MessageToMessageEncoder<ServerPacket>
    {
        private readonly ILogger logger;
        private readonly PacketFactory packetFactory;

        public Serializer(ILogger logger, PacketFactory packetFactory)
        {
            this.logger = logger;
            this.packetFactory = packetFactory;
        }

        protected override void Encode(IChannelHandlerContext context, ServerPacket message, List<object> output)
        {
            var packet = packetFactory.CreatePacket(message);
            if (string.IsNullOrEmpty(packet))
            {
                logger.Debug("Empty packet, skipping it");
                return;
            }

#if(DEBUG)
            logger.Debug($"Out: {ObjectDumper.Dump(message)}");
#endif
            
            output.Add(packet);
        }
    }
}