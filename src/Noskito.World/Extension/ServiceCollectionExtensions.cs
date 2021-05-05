using Microsoft.Extensions.DependencyInjection;
using Noskito.World.Game.Maps;
using Noskito.World.Network;

namespace Noskito.World.Extension
{
    public static class ServiceCollectionExtensions
    {
        public static void AddNetworkServer(this IServiceCollection services)
        {
            services.AddTransient<NetworkServer>();
        }

        public static void AddMapManager(this IServiceCollection services)
        {
            services.AddSingleton<MapManager>();
        }
    }
}