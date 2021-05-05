using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Noskito.Common.Logging;
using Noskito.Communication.Abstraction.Server;
using Noskito.Communication.Server;
using Noskito.World.Network;

namespace Noskito.World
{
    public class WorldService : IHostedService
    {
        private readonly ILogger logger;
        private readonly NetworkServer server;
        private readonly ServerService serverService;

        public WorldService(ILogger logger, NetworkServer server, ServerService serverService)
        {
            this.logger = logger;
            this.server = server;
            this.serverService = serverService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            bool clusterOnline;
            do
            {
                clusterOnline = await serverService.IsClusterOnline();
            } while (!clusterOnline);

            var added = await serverService.AddWorldServer(new WorldServer
            {
                Host = "127.0.0.1",
                Port = 20000,
                Name = "Noskito"
            });

            if (!added)
            {
                logger.Warning("Failed to register world server");
                return;
            }

            logger.Information("Starting server");
            await server.Start(20000);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            logger.Information("Stopping server");
            await server.Stop();
        }
    }
}