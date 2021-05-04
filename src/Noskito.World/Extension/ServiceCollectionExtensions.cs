using Microsoft.Extensions.DependencyInjection;
using Noskito.World.Network;

namespace Noskito.World.Extension
{
    public static class ServiceCollectionExtensions
    {
        public static void AddNetworkServer(this IServiceCollection services)
        {
            services.AddTransient<NetworkServer>();
        }
    }
}