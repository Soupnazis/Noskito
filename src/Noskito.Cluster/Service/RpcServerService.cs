using System.Linq;
using System.Threading.Tasks;
using Noskito.Cluster.Manager;
using Noskito.Common.Logging;
using Noskito.Communication.Abstraction.Server;
using Noskito.Communication.Rpc.Common.Request;
using Noskito.Communication.Rpc.Common.Response;
using Noskito.Communication.Rpc.Server;
using Noskito.Communication.Rpc.Server.Object;
using Noskito.Communication.Rpc.Server.Request;
using Noskito.Communication.Rpc.Server.Response;

namespace Noskito.Cluster.Service
{
    public class RpcServerService : IRpcServerService
    {
        private readonly ILogger logger;
        private readonly ServerManager serverManager;

        public RpcServerService(ILogger logger, ServerManager serverManager)
        {
            this.logger = logger;
            this.serverManager = serverManager;
        }

        public ValueTask<BoolResponse> IsClusterOnline(EmptyRequest request)
        {
            return ValueTask.FromResult(new BoolResponse
            {
                Value = true
            });
        }

        public ValueTask<BoolResponse> IsMaintenanceMode(EmptyRequest request)
        {
            return ValueTask.FromResult(new BoolResponse
            {
                Value = serverManager.IsMaintenanceMode
            });
        }

        public ValueTask<GetWorldServersResponse> GetWorldServers(EmptyRequest request)
        {
            return ValueTask.FromResult(new GetWorldServersResponse
            {
                Servers = serverManager.GetWorldServers().Select(x => new WorldServerObject
                {
                    Host = x.Host,
                    Port = x.Port,
                    Name = x.Name
                })
            });
        }

        public ValueTask<BoolResponse> AddWorldServer(AddWorldServerRequest request)
        {
            var server = request.Server;
            var added = serverManager.AddWorldServer(new WorldServer
            {
                Host = server.Host,
                Port = server.Port,
                Name = server.Name
            });

            return ValueTask.FromResult(new BoolResponse
            {
                Value = added
            });
        }
    }
}