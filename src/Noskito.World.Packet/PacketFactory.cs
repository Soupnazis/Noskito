using System;
using System.Collections.Generic;
using System.Linq;
using Noskito.World.Packet.Client;
using Noskito.World.Packet.Server;

namespace Noskito.World.Packet
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
            if (split.Length == 0)
            {
                throw new InvalidOperationException("Empty packet received");
            }

            if (!int.TryParse(split[0], out int packetId))
            {
                throw new InvalidOperationException("Failed to parse packet id");
            }
            
            var header = split[1];
            var parameters = split.Length > 2 ? split.Skip(2).ToArray() : Array.Empty<string>();

            var creator = clientPackets.GetValueOrDefault(header);
            if (creator == null)
            {
                return new UnresolvedPacket
                {
                    PacketId = packetId,
                    Header = header,
                    Parameters = parameters
                };
            }

            var packet = creator.Create(parameters);

            packet.PacketId = packetId;

            return packet;
        }

        public string CreatePacket(SPacket source)
        {
            var creator = serverPackets.GetValueOrDefault(source.GetType());
            if (creator == null)
            {
                throw new InvalidOperationException($"There is no packet creator for {source.GetType()}");
            }

            return creator.Create(source);
        }
    }
}