using System.Collections.Generic;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using Noskito.Common.Logging;
using Noskito.Login.Packet;
using Noskito.Login.Packet.Server;

namespace Noskito.Login.Network.Pipeline
{
    public class Serializer : MessageToMessageEncoder<SPacket>
    {
        private readonly ILogger logger;
        private readonly PacketFactory packetFactory;

        public Serializer(ILogger logger, PacketFactory packetFactory)
        {
            this.logger = logger;
            this.packetFactory = packetFactory;
        }

        protected override void Encode(IChannelHandlerContext context, SPacket message, List<object> output)
        {
            var packet = packetFactory.CreatePacket(message);
            if (string.IsNullOrEmpty(packet))
            {
                logger.Debug("Empty packet, skipping it");
                return;
            }

            output.Add(packet);

            logger.Debug($"Out [{message.GetType().Name}]: {packet}");
        }
    }
}