using System;
using System.Collections.Generic;
using System.Linq;
using Noskito.Packet.Client;
using Noskito.Packet.Server;

namespace Noskito.Packet
{
    public class PacketFactory
    {
        private readonly Dictionary<string, IClientPacketCreator> clientPacketCreators;
        private readonly Dictionary<Type, IServerPacketCreator> serverPacketCreators;

        public PacketFactory(IEnumerable<IClientPacketCreator> clientPacketCreators, IEnumerable<IServerPacketCreator> serverPacketCreators)
        {
            this.clientPacketCreators = clientPacketCreators.ToDictionary(x => x.Header);
            this.serverPacketCreators = serverPacketCreators.ToDictionary(x => x.PacketType);
        }

        public ClientPacket CreatePacket(string packet)
        {
            var split = packet.Split(' ');

            var header = split[0];
            var arguments = split.Skip(1).ToArray();

            var creator = clientPacketCreators.GetValueOrDefault(header);
            if (creator == null)
            {
                return new UnresolvedPacket();
            }

            return creator.CreatePacket(arguments);
        }

        public string CreatePacket(ServerPacket packet)
        {
            var creator = serverPacketCreators.GetValueOrDefault(packet.GetType());
            if (creator == null)
            {
                throw new InvalidOperationException($"Can't found packet creator for {packet.GetType()}");
            }

            return creator.CreatePacket(packet);
        }
    }
}