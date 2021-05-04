using System;
using System.Threading.Tasks;
using DotNetty.Transport.Channels;
using Noskito.Common.Logging;
using Noskito.Database.Abstraction.Entity;
using Noskito.World.Abstraction.Network;
using Noskito.World.Packet.Client;
using Noskito.World.Packet.Server;
using Noskito.World.Processor;

namespace Noskito.World.Network
{
    public class WorldClient : ChannelHandlerAdapter, IWorldClient
    {
        private readonly ILogger logger;
        private readonly IChannel channel;
        private readonly ProcessorManager processorManager;

        public WorldClient(ILogger logger, IChannel channel, ProcessorManager processorManager)
        {
            Id = Guid.NewGuid();
            
            this.logger = logger;
            this.channel = channel;
            this.processorManager = processorManager;
        }

        public Guid Id { get; }
        public int EncryptionKey { get; set; }
        public string Username { get; set; }
        public Account Account { get; set; }

        public Task SendPacket<T>(T packet) where T : SPacket
        {
            return channel.WriteAndFlushAsync(packet);
        }

        public Task Disconnect()
        {
            return channel.DisconnectAsync();
        }

        public Task Process<T>(T packet) where T : CPacket
        {
            IPacketProcessor processor = processorManager.GetPacketProcessor(typeof(T));
            if (processor == null)
            {
                return Task.CompletedTask;
            }

            return processor.ProcessPacket(this, packet);
        }

        public override async void ChannelRead(IChannelHandlerContext context, object message)
        {
            if (message is not CPacket packet)
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
                await Process(packet);
            }
            catch (Exception e)
            {
                logger.Error($"Something happened when processing packet {packet.GetType().Name}", e);
            }
        }
    }
}