using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Noskito.Communication.Abstraction.Server;
using Noskito.Communication.Rpc.Common.Request;
using Noskito.Communication.Rpc.Server;
using Noskito.Communication.Rpc.Server.Object;
using Noskito.Communication.Rpc.Server.Request;

namespace Noskito.Communication.Server
{
    public class ServerService
    {
        private readonly IRpcServerService rpc;

        public ServerService(IRpcServerService rpc)
        {
            this.rpc = rpc;
        }

        public async Task<bool> IsClusterOnline()
        {
            try
            {
                var response = await rpc.IsClusterOnline(new EmptyRequest());
                return response.Value;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> IsMaintenanceMode()
        {
            var response = await rpc.IsMaintenanceMode(new EmptyRequest());
            return response.Value;
        }

        public async Task<IEnumerable<WorldServer>> GetWorldServers()
        {
            var response = await rpc.GetWorldServers(new EmptyRequest());
            return response.Servers.Select(x => new WorldServer
            {
                Host = x.Host,
                Port = x.Port,
                Name = x.Name
            });
        }

        public async Task<bool> AddWorldServer(WorldServer server)
        {
            var response = await rpc.AddWorldServer(new AddWorldServerRequest
            {
                Server = new WorldServerObject
                {
                    Host = server.Host,
                    Port = server.Port,
                    Name = server.Name
                }
            });
            return response.Value;
        }
    }
}