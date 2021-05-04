using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Noskito.Common.Logging;
using Noskito.Communication.Abstraction.Server;
using Noskito.World.Abstraction.Network;

namespace Noskito.World
{
    public class WorldService : IHostedService
    {
        private readonly ILogger logger;
        private readonly IWorldServer server;
        private readonly IServerService serverService;

        public WorldService(ILogger logger, IWorldServer server, IServerService serverService)
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
            } 
            while (!clusterOnline);
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