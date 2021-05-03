using Microsoft.Extensions.DependencyInjection;
using Noskito.Communication.Abstraction.Server;
using Noskito.Communication.Rpc.Extension;
using Noskito.Communication.Server;

namespace Noskito.Communication.Extension
{
    public static class ServiceCollectionExtensions
    {
        public static void AddServerService(this IServiceCollection services)
        {
            services.AddRpcServerService();
            services.AddTransient<IServerService, ServerService>();
        }
    }
}