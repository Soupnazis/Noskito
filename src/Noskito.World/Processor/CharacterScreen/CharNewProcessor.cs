using System.Collections.Generic;
using System.Threading.Tasks;
using Noskito.Database.Dto;
using Noskito.Database.Repository;
using Noskito.World.Packet.Client.CharacterScreen;
using Noskito.World.Packet.Server.CharacterScreen;

namespace Noskito.World.Processor.CharacterScreen
{
    public class CharNewProcessor : PacketProcessor<CharNew>
    {
        private readonly CharacterRepository characterRepository;

        public CharNewProcessor(CharacterRepository characterRepository)
        {
            this.characterRepository = characterRepository;
        }

        protected override async Task Process(WorldSession client, CharNew packet)
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

            await characterRepository.Create(new CharacterDTO
            {
                Name = packet.Name,
                Slot = packet.Slot,
                AccountId = client.Account.Id,
                Gender = packet.Gender,
                HairColor = packet.HairColor,
                HairStyle = packet.HairStyle,
            });

            await client.SendPacket(new Success());
            
            IEnumerable<CharacterDTO> characters = await characterRepository.FindAll(client.Account.Id);

            await client.SendPacket(new CListStart());
            foreach (var character in characters)
            {
                await client.SendPacket(new CList
                {
                    Name = character.Name,
                    Slot = character.Slot,
                    HairColor = character.HairColor,
                    HairStyle = character.HairStyle,
                    Level = character.Level,
                    Gender = character.Gender,
                    HeroLevel = 0,
                    JobLevel = 0,
                    Rename = false
                });
            }
            await client.SendPacket(new CListEnd());
        }
    }
}