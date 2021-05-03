using System.Collections.Generic;

namespace Noskito.Packet.Server.Authentication
{
    public class NsTeSt : ServerPacket
    {
        public byte RegionId { get; init; }
        public string Account { get; init; }
        public int SessionId { get; init; }
        public List<Server> Servers { get; init; } = new();
        
        public class Server
        {
            public string Host { get; init; }
            public int Port { get; init; }
            public int Color { get; init; }
            public int Count { get; init; }
            public int Id { get; init; }
            public string Name { get; init; }
        }
    }

    public class NsTeStCreator : ServerPacketCreator<NsTeSt>
    {
        protected override string Create(NsTeSt source)
        {
            var packet = $"NsTeST {source.RegionId} {source.Account} {source.SessionId} ";
            foreach (var server in source.Servers)
            {
                packet += $"{server.Host}:{server.Port}:{server.Color}:{server.Count}.{server.Id}.{server.Name}";
            }

            return packet;
        }
    }
}