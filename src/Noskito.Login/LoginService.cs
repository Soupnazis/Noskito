using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Noskito.Common.Logging;
using Noskito.Communication.Server;
using Noskito.Login.Network;

namespace Noskito.Login
{
    public class LoginService : IHostedService
    {
        private readonly ILogger logger;
        private readonly NetworkServer server;
        private readonly ServerService serverService;

        public LoginService(ILogger logger, NetworkServer server, ServerService serverService)
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

            logger.Information("Starting server");
            await server.Start(10000);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            logger.Information("Stopping server");
            await server.Stop();
        }
    }
}