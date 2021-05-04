using Microsoft.Extensions.DependencyInjection;
using Noskito.Common.Extension;
using Noskito.World.Packet.Client;
using Noskito.World.Packet.Server;

namespace Noskito.World.Packet.Extension
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPacketFactory(this IServiceCollection services)
        {
            services.AddSingleton<PacketFactory>();
            services.AddImplementingTypes<SPacketCreator>();
            services.AddImplementingTypes<CPacketCreator>();
        }
    }
}