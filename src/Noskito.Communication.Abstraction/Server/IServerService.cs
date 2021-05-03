using System.Collections.Generic;
using System.Threading.Tasks;

namespace Noskito.Communication.Abstraction.Server
{
    public interface IServerService
    {
        Task<bool> IsClusterOnline();
        Task<bool> IsMaintenanceMode();
        Task<IEnumerable<WorldServer>> GetWorldServers();
        Task<bool> AddWorldServer(WorldServer server);
    }
}