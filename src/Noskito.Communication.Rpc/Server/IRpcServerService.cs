using System.ServiceModel;
using System.Threading.Tasks;
using Noskito.Communication.Rpc.Common.Request;
using Noskito.Communication.Rpc.Common.Response;
using Noskito.Communication.Rpc.Server.Request;
using Noskito.Communication.Rpc.Server.Response;

namespace Noskito.Communication.Rpc.Server
{
    [ServiceContract]
    public interface IRpcServerService
    {
        [OperationContract]
        ValueTask<BoolResponse> IsClusterOnline(EmptyRequest request);

        [OperationContract]
        ValueTask<BoolResponse> IsMaintenanceMode(EmptyRequest request);

        [OperationContract]
        ValueTask<GetWorldServersResponse> GetWorldServers(EmptyRequest request);

        [OperationContract]
        ValueTask<BoolResponse> AddWorldServer(AddWorldServerRequest request);
    }
}