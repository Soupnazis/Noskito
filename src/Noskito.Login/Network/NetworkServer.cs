using System.Threading.Tasks;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using Noskito.Common.Logging;
using Noskito.Login.Network.Pipeline;
using Noskito.Login.Packet;
using Noskito.Login.Processor;

namespace Noskito.Login.Network
{
    public sealed class NetworkServer
    {
        private readonly ServerBootstrap bootstrap;
        private readonly MultithreadEventLoopGroup bossGroup, workerGroup;
        private readonly ILogger logger;

        private IChannel channel;

        public NetworkServer(ILogger logger, PacketFactory packetFactory, ProcessorManager processorManager)
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

                    var client = new NetworkClient(logger, x);
                    var session = new LoginSession(client);

                    client.PacketReceived += packet =>
                    {
                        var processor = processorManager.GetPacketProcessor(packet.GetType());
                        if (processor is null) return Task.CompletedTask;

                        return processor.ProcessPacket(session, packet);
                    };

                    pipeline.AddLast("decoder", new Decoder(logger));
                    pipeline.AddLast("deserializer", new Deserializer(logger, packetFactory));
                    pipeline.AddLast("client", client);
                    pipeline.AddLast("encoder", new Encoder(logger));
                    pipeline.AddLast("serializer", new Serializer(logger, packetFactory));
                }));
        }

        public async Task Start(int port)
        {
            channel = await bootstrap.BindAsync(port);

            logger.Debug($"Server successfully started on port {port}");
        }

        public async Task Stop()
        {
            await channel.CloseAsync();

            await bossGroup.ShutdownGracefullyAsync();
            await workerGroup.ShutdownGracefullyAsync();

            logger.Debug("Server successfully shutdown");
        }
    }
}