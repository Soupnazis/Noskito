using System;
using System.Threading.Tasks;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using Noskito.Common.Logging;
using Noskito.Login.Abstraction.Network;
using Noskito.Login.Network.Pipeline;
using Noskito.Login.Processor;
using Noskito.Packet;

namespace Noskito.Login.Network
{
    public sealed class LoginServer : ILoginServer
    {
        private readonly ILogger logger;
        private readonly ServerBootstrap bootstrap;
        private readonly MultithreadEventLoopGroup bossGroup, workerGroup;

        private IChannel channel;
        
        public LoginServer(ILogger logger, PacketFactory packetFactory, ProcessorManager processorManager)
        {
            this.logger = logger;

            bossGroup = new MultithreadEventLoopGroup(1);
            workerGroup = new MultithreadEventLoopGroup();
            
            bootstrap = new ServerBootstrap()
                .Option(ChannelOption.SoBacklog, 100)
                .Group(bossGroup, workerGroup)
                .Channel<TcpServerSocketChannel>()
                .ChildHandler(new ActionChannelInitializer<IChannel>(x =>
                {
                    var pipeline = x.Pipeline;

                    var client = new LoginClient(logger, x, processorManager);

                    pipeline.AddLast("decoder", new Decoder(logger));
                    pipeline.AddLast("deserializer", new Deserializer(logger, packetFactory));
                    pipeline.AddLast("client", client);
                    pipeline.AddLast("encoder", new Encoder(logger));
                    pipeline.AddLast("serializer", new Serializer(logger, packetFactory));
                }));
        }

        public bool IsOnline => channel?.Active == true;
        
        public async Task Start(int port)
        {
            if (IsOnline)
            {
                throw new InvalidOperationException("Can't start already started server");
            }
            
            channel = await bootstrap.BindAsync(port);
            
            logger.Debug($"Server successfully started on port {port}");
        }

        public async Task Stop()
        {
            if (!IsOnline)
            {
                throw new InvalidOperationException("Can't stop a not started server");
            }
            
            await channel.CloseAsync();

            await bossGroup.ShutdownGracefullyAsync();
            await workerGroup.ShutdownGracefullyAsync();
            
            logger.Debug("Server successfully shutdown");
        }
    }
}