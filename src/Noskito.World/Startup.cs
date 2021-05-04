using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Noskito.Common.Extension;
using Noskito.Communication.Extension;
using Noskito.Database.Extension;
using Noskito.World.Extension;
using Noskito.World.Packet.Extension;
using Noskito.World.Processor.Extension;

namespace Noskito.World
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogger();
            services.AddPacketFactory();
            services.AddPacketProcessing();

            services.AddDatabase();

            services.AddServerService();
            
            services.AddWorldServer();

            services.AddHostedService<WorldService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
        }
    }
}