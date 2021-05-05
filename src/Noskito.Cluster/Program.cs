using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Noskito.Common.Extension;
using ProtoBuf.Grpc.Server;

namespace Noskito.Cluster
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(x =>
                {
                    x.AddLogger();

                    x.AddGrpc();
                    x.AddCodeFirstGrpc();
                })
                .ConfigureWebHost(x =>
                {
                    x.UseKestrel(s =>
                    {
                        s.ListenLocalhost(15000, options => options.Protocols = HttpProtocols.Http2);
                    });
                    x.UseStartup<Startup>();
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