using Noskito.Communication.Rpc.Server.Object;
using ProtoBuf;

namespace Noskito.Communication.Rpc.Server.Request
{
    [ProtoContract]
    public class AddWorldServerRequest
    {
        [ProtoMember(1)] public WorldServerObject Server { get; init; }
    }
}