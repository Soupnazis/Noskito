using Microsoft.Extensions.DependencyInjection;
using Noskito.World.Abstraction.Network;
using Noskito.World.Network;

namespace Noskito.World.Extension
{
    public static class ServiceCollectionExtensions
    {
        public static void AddWorldServer(this IServiceCollection services)
        {
            services.AddTransient<IWorldServer, WorldServer>();
        }
    }
}