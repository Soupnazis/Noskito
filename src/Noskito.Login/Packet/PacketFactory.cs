using System;
using System.Collections.Generic;
using System.Linq;
using Noskito.Login.Packet.Client;
using Noskito.Login.Packet.Server;

namespace Noskito.Login.Packet
{
    public class PacketFactory
    {
        private readonly Dictionary<string, CPacketCreator> clientPackets;
        private readonly Dictionary<Type, SPacketCreator> serverPackets;

        public PacketFactory(IEnumerable<CPacketCreator> clientPackets, IEnumerable<SPacketCreator> serverPackets)
        {
            this.clientPackets = clientPackets.ToDictionary(x => x.Header);
            this.serverPackets = serverPackets.ToDictionary(x => x.PacketType);
        }

        public CPacket CreatePacket(string source)
        {
            var split = source.Split(' ');
            if (split.Length == 0) throw new InvalidOperationException("Empty packet received");

            var header = split[0];
            var parameters = split.Length > 1 ? split.Skip(1).ToArray() : Array.Empty<string>();

            var creator = clientPackets.GetValueOrDefault(header);
            if (creator == null)
                return new UnresolvedPacket
                {
                    Header = header,
                    Parameters = parameters
                };

            return creator.Create(parameters);
        }

        public string CreatePacket(SPacket source)
        {
            var creator = serverPackets.GetValueOrDefault(source.GetType());
            if (creator == null)
                throw new InvalidOperationException($"There is no packet creator for {source.GetType()}");

            return creator.Create(source);
        }
    }
}