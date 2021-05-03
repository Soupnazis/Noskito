using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Noskito.Common.Logging;
using Noskito.Login.Abstraction;
using Noskito.Login.Abstraction.Network;

namespace Noskito.Login
{
    public class LoginService : ILoginService, IHostedService
    {
        private readonly ILogger logger;
        private readonly ILoginServer server;

        public LoginService(ILogger logger, ILoginServer server)
        {
            this.logger = logger;
            this.server = server;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
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