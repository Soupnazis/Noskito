using Microsoft.Extensions.DependencyInjection;
using Noskito.Common.Extension;
using Noskito.Packet.Client;
using Noskito.Packet.Server;

namespace Noskito.Packet.Extension
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPacketFactory(this IServiceCollection services)
        {
            services.AddSingleton<PacketFactory>();
            services.AddImplementingTypes<IClientPacketCreator>();
            services.AddImplementingTypes<IServerPacketCreator>();
        }
    }
}