using Microsoft.Extensions.DependencyInjection;
using Noskito.Login.Network;

namespace Noskito.Login.Extension
{
    public static class ServiceCollectionExtensions
    {
        public static void AddNetworkServer(this IServiceCollection services)
        {
            services.AddTransient<NetworkServer>();
        }
    }
}