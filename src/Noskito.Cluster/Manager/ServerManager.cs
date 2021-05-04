using System.Collections.Generic;
using System.Linq;
using Noskito.Communication.Abstraction.Server;

namespace Noskito.Cluster.Manager
{
    public class ServerManager
    {
        public bool IsMaintenanceMode { get; set; } = false;

        private readonly List<WorldServer> servers = new();
        
        public IEnumerable<WorldServer> GetWorldServers()
        {
            return servers;
        }

        public bool AddWorldServer(WorldServer server)
        {
            var exists = servers.Any(x => x.Host.Equals(server.Host) && x.Port.Equals(server.Port));
            if (exists)
            {
                return false;
            }
            
            servers.Add(server);
            return true;
        }
    }
}