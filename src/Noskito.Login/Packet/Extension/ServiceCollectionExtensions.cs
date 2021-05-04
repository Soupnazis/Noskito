using Microsoft.Extensions.DependencyInjection;
using Noskito.Common.Extension;
using Noskito.Login.Packet.Client;
using Noskito.Login.Packet.Server;

namespace Noskito.Login.Packet.Extension
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