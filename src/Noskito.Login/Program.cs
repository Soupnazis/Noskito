using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Noskito.Common.Extension;
using Noskito.Database.Extension;
using Noskito.Login.Extension;
using Noskito.Login.Processor.Extension;
using Noskito.Packet.Extension;

namespace Noskito.Login
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(x =>
                {
                    x.AddLogger();
                    x.AddPacketFactory();
                    x.AddPacketProcessing();
                    
                    x.AddDatabase();
                    
                    x.AddLoginServer();

                    x.AddHostedService<LoginService>();
                })
                .ConfigureLogging(x =>
                {
                    x.ClearProviders();
                    x.AddFilter("Microsoft", LogLevel.Warning);
                })
                .UseConsoleLifetime()
                .Build();

            using (host)
            {
                await host.StartAsync();
                await host.WaitForShutdownAsync();
            }
        }
    }
}