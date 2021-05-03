using System;
using System.Threading.Tasks;
using DotNetty.Transport.Channels;
using Noskito.Common.Logging;
using Noskito.Login.Abstraction.Network;
using Noskito.Login.Processor;
using Noskito.Packet.Client;
using Noskito.Packet.Server;

namespace Noskito.Login.Network
{
    public sealed class LoginClient : ChannelHandlerAdapter, ILoginClient
    {
        private readonly ILogger logger;
        private readonly IChannel channel;
        private readonly ProcessorManager processorManager;

        public LoginClient(ILogger logger, IChannel channel, ProcessorManager processorManager)
        {
            Id = Guid.NewGuid();
            
            this.logger = logger;
            this.channel = channel;
            this.processorManager = processorManager;
        }

        public Guid Id { get; }

        public Task SendPacket(ServerPacket packet)
        {
            return channel.WriteAndFlushAsync(packet);
        }

        public Task Disconnect()
        {
            return channel.DisconnectAsync();
        }

        public override async void ChannelRead(IChannelHandlerContext context, object message)
        {
            if (message is not ClientPacket packet)
            {
                logger.Debug("Received a non ClientPacket message");
                return;
            }

            var processor = processorManager.GetPacketProcessor(packet.GetType());
            if (processor == null)
            {
                logger.Debug($"No processor found for packet {packet.GetType()}");
                return;
            }

            try
            {
                await processor.ProcessPacket(this, packet);
            }
            catch (Exception e)
            {
                logger.Error($"Something happened when processing packet {packet.GetType().Name}", e);
            }
        }
    }
}