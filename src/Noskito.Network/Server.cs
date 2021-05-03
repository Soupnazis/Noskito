using System.Threading.Tasks;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using Noskito.Common.Logging;

namespace Noskito.Network
{
    public abstract class Server
    {
        private readonly ILogger logger;
        private readonly IClientFactory clientFactory;
        private readonly ServerBootstrap bootstrap;

        private IChannel channel;
        
        protected Server(ILogger logger, IClientFactory clientFactory)
        {
            this.logger = logger;
            this.clientFactory = clientFactory;
            
            bootstrap = new ServerBootstrap()
                .Option(ChannelOption.SoBacklog, 100)
                .Channel<TcpServerSocketChannel>()
                .ChildHandler(new ActionChannelInitializer<IChannel>(x =>
                {
                    var pipeline = x.Pipeline;
                    var client = clientFactory.CreateClient(x);

                    pipeline.AddLast(client);
                }));
        }

        public async Task Start(int port)
        {
            channel = await bootstrap.BindAsync(port);
        }

        public async Task Stop()
        {
            await channel.CloseAsync();
        }
    }
}