using ProtoBuf;

namespace Noskito.Communication.Rpc.Common.Response
{
    [ProtoContract]
    public class BoolResponse
    {
        [ProtoMember(1)] public bool Value { get; set; }
    }
}