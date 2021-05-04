﻿using System.Collections.Generic;
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

        protected override async Task Process(WorldSession session, CharNew packet)
        {
            if (session.Account == null)
            {
                return;
            }
            
            var slotTaken = await characterRepository.IsSlotTaken(session.Account.Id, packet.Slot);
            if (slotTaken)
            {
                await session.Disconnect();
                return;
            }
            
            var nameTaken = await characterRepository.IsNameTaken(packet.Name);
            if (nameTaken)
            {
                await session.Disconnect();
                return;
            }

            await characterRepository.Create(new CharacterDTO
            {
                Name = packet.Name,
                Slot = packet.Slot,
                AccountId = session.Account.Id,
                Gender = packet.Gender,
                HairColor = packet.HairColor,
                HairStyle = packet.HairStyle,
            });

            await session.SendPacket(new Success());
            
            IEnumerable<CharacterDTO> characters = await characterRepository.FindAll(session.Account.Id);

            await session.SendPacket(new CListStart());
            foreach (var character in characters)
            {
                await session.SendPacket(new CList
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
            await session.SendPacket(new CListEnd());
        }
    }
}