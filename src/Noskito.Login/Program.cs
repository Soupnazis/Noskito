using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Noskito.Login
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureLogging(x =>
                {
                    x.ClearProviders();
                    x.AddFilter("Microsoft", LogLevel.Warning);
                })
                .ConfigureWebHostDefaults(x =>
                {
                    x.UseKestrel(s => { s.ListenAnyIP(14000, options => options.Protocols = HttpProtocols.Http2); });
                    x.UseStartup<Startup>();
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