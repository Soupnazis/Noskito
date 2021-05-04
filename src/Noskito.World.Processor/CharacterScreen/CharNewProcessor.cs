using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Noskito.Database.Abstraction.Entity;
using Noskito.Database.Abstraction.Repository;
using Noskito.Enum.Character;
using Noskito.World.Abstraction.Network;
using Noskito.World.Packet.Client.CharacterScreen;
using Noskito.World.Packet.Server.CharacterScreen;

namespace Noskito.World.Processor.CharacterScreen
{
    public class CharNewProcessor : PacketProcessor<CharNew>
    {
        private readonly ICharacterRepository characterRepository;

        public CharNewProcessor(ICharacterRepository characterRepository)
        {
            this.characterRepository = characterRepository;
        }

        protected override async Task Process(IWorldClient client, CharNew packet)
        {
            if (client.Account == null)
            {
                return;
            }
            
            var slotTaken = await characterRepository.IsSlotTaken(client.Account.Id, packet.Slot);
            if (slotTaken)
            {
                await client.Disconnect();
                return;
            }
            
            var nameTaken = await characterRepository.IsNameTaken(packet.Name);
            if (nameTaken)
            {
                await client.Disconnect();
                return;
            }

            await characterRepository.Create(new Character
            {
                Name = packet.Name,
                Slot = packet.Slot,
                AccountId = client.Account.Id,
                Gender = packet.Gender,
                HairColor = packet.HairColor,
                HairStyle = packet.HairStyle,
            });

            await client.SendPacket(new Success());
            await client.Process(new EntryPoint());
        }
    }
}