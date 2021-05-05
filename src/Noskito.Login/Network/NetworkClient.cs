using System;
using System.Threading.Tasks;
using DotNetty.Transport.Channels;
using Noskito.Common.Logging;
using Noskito.Login.Packet.Client;
using Noskito.Login.Packet.Server;
using Noskito.Login.Processor;

namespace Noskito.Login.Network
{
    public sealed class NetworkClient : ChannelHandlerAdapter
    {
        private readonly ILogger logger;
        private readonly IChannel channel;

        public event Func<CPacket, Task> PacketReceived; 
        
        public NetworkClient(ILogger logger, IChannel channel)
        {
            this.logger = logger;
            this.channel = channel;
        }

        public Task SendPacket<T>(T packet) where T : SPacket
        {
            return channel.WriteAndFlushAsync(packet);
        }

        public Task Disconnect()
        {
            return channel.DisconnectAsync();
        }

        public override async void ChannelRead(IChannelHandlerContext context, object message)
        {
            if (message is not CPacket packet)
            {
                logger.Debug("Received a non ClientPacket message");
                return;
            }

            try
            {
                if (PacketReceived != null)
                {
                    await PacketReceived?.Invoke(packet);
                }
            }
            catch (Exception e)
            {
                logger.Error($"Something happened when processing packet {packet.GetType().Name}", e);
            }
        }
    }
}