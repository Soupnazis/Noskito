using System.Collections.Generic;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using Noskito.Common.Logging;
using Noskito.Login.Packet;

namespace Noskito.Login.Network.Pipeline
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
            if (packet is null)
            {
                logger.Debug("Failed to create typed packet, skipping it");
                return;
            }

            output.Add(packet);

            logger.Debug($"In [{packet.GetType().Name}]: {message}");
        }
    }
}