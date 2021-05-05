using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Noskito.Common.Extension;
using Noskito.Communication.Extension;
using Noskito.Database.Extension;
using Noskito.Login.Extension;
using Noskito.Login.Packet.Extension;
using Noskito.Login.Processor.Extension;

namespace Noskito.Login
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

            services.AddNetworkServer();

            services.AddHostedService<LoginService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseRouting();
        }
    }
}