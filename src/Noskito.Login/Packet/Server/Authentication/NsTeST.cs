using System.Collections.Generic;

namespace Noskito.Login.Packet.Server.Authentication
{
    public class NsTeST : SPacket
    {
        public byte RegionId { get; init; }
        public string Account { get; init; }
        public int EncryptionKey { get; init; }
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

    public class NsTeSTCreator : SPacketCreator<NsTeST>
    {
        protected override string CreatePacket(NsTeST source)
        {
            var packet = $"NsTeST {source.RegionId} {source.Account} {source.EncryptionKey} ";
            foreach (var server in source.Servers)
                packet += $"{server.Host}:{server.Port}:{server.Color}:{server.Id}.{server.Count}.{server.Name} ";

            packet += "-1:-1:-1:10000.10000.1";

            return packet;
        }
    }
}