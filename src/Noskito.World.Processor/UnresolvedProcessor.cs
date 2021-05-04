using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Noskito.Common.Extension;
using Noskito.Common.Logging;
using Noskito.Database.Abstraction.Entity;
using Noskito.Database.Abstraction.Repository;
using Noskito.World.Abstraction.Network;
using Noskito.World.Packet.Client;
using Noskito.World.Packet.Client.CharacterScreen;
using Noskito.World.Packet.Server.CharacterScreen;

namespace Noskito.World.Processor
{
    public class UnresolvedProcessor : PacketProcessor<UnresolvedPacket>
    {
        protected override async Task Process(IWorldClient client, UnresolvedPacket packet)
        {
            if (client.EncryptionKey == 0)
            {
                client.EncryptionKey = int.Parse(packet.Header);
                return;
            }

            if (client.Username == null)
            {
                client.Username = packet.Header;
                return;
            }

            if (client.Account == null)
            {
                await client.Process(new EntryPoint
                {
                    Password = packet.Header
                });
            }
        }
    }
}