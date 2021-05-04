using Grpc.Net.Client;
using Microsoft.Extensions.DependencyInjection;
using Noskito.Communication.Rpc.Server;
using ProtoBuf.Grpc.Client;

namespace Noskito.Communication.Rpc.Extension
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRpcServerService(this IServiceCollection services)
        {
            var channel = GrpcChannel.ForAddress("http://localhost:15000");
            var service = channel.CreateGrpcService<IRpcServerService>();

            services.AddSingleton(service);
        }
    }
}