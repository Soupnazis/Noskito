using System.Collections.Generic;
using Noskito.Communication.Rpc.Server.Object;
using ProtoBuf;

namespace Noskito.Communication.Rpc.Server.Response
{
    [ProtoContract]
    public class GetWorldServersResponse
    {
        [ProtoMember(1)] public IEnumerable<WorldServerObject> Servers { get; init; }
    }
}