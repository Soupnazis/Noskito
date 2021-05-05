using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Noskito.Cluster.Manager;
using Noskito.Cluster.Service;
using Noskito.Common.Extension;
using ProtoBuf.Grpc.Server;

namespace Noskito.Cluster
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogger();
            services.AddCodeFirstGrpc();

            services.AddSingleton<ServerManager>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapGrpcService<RpcServerService>(); });
        }
    }
}