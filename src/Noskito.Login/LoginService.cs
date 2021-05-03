using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Noskito.Common.Logging;
using Noskito.Communication.Abstraction.Server;
using Noskito.Login.Abstraction;
using Noskito.Login.Abstraction.Network;

namespace Noskito.Login
{
    public class LoginService : ILoginService, IHostedService
    {
        private readonly ILogger logger;
        private readonly ILoginServer server;
        private readonly IServerService serverService;

        public LoginService(ILogger logger, ILoginServer server, IServerService serverService)
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
            
            logger.Information("Starting server");
            await server.Start(9999);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            logger.Information("Stopping server");
            await server.Stop();
        }
    }
}