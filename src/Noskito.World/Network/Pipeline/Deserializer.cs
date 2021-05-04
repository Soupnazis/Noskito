using System.Collections.Generic;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using Noskito.Common.Logging;
using Noskito.World.Packet;

namespace Noskito.World.Network.Pipeline
{
    public class Deserializer : MessageToMessageDecoder<string>
    {
        private readonly ILogger logger;
        private readonly PacketFactory packetFactory;

        public Deserializer(ILogger logger, PacketFactory packetFactory)
        {
            this.logger = logger;
            this.packetFactory = packetFactory;
        }

        protected override void Decode(IChannelHandlerContext context, string message, List<object> output)
        {
            var packet = packetFactory.CreatePacket(message);
            if (packet == null)
            {
                logger.Debug("Failed to create typed packet, skipping it");
                return;
            }
            
#if(DEBUG)
            logger.Debug($"{message}");
            logger.Debug($"In: {ObjectDumper.Dump(packet)}");
#endif
            
            output.Add(packet);
        }
    }
}