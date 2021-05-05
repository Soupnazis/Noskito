using ProtoBuf;

namespace Noskito.Communication.Rpc.Server.Object
{
    [ProtoContract]
    public class WorldServerObject
    {
        [ProtoMember(1)] public string Name { get; init; }

        [ProtoMember(2)] public string Host { get; init; }

        [ProtoMember(3)] public int Port { get; init; }
    }
}