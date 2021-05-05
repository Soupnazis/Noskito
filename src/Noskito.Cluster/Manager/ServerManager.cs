using System.Collections.Generic;
using System.Linq;
using Noskito.Communication.Abstraction.Server;

namespace Noskito.Cluster.Manager
{
    public class ServerManager
    {
        private readonly List<WorldServer> servers = new();
        public bool IsMaintenanceMode { get; set; } = false;

        public IEnumerable<WorldServer> GetWorldServers()
        {
            return servers;
        }

        public bool AddWorldServer(WorldServer server)
        {
            var exists = servers.Any(x => x.Host.Equals(server.Host) && x.Port.Equals(server.Port));
            if (exists) return false;

            servers.Add(server);
            return true;
        }
    }
}