using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Noskito.Common.Extension;
using Noskito.Common.Logging;
using Noskito.Database.Dto;
using Noskito.Database.Repository;
using Noskito.World.Packet.Client;
using Noskito.World.Packet.Server.CharacterScreen;

namespace Noskito.World.Processor
{
    public class UnresolvedProcessor : PacketProcessor<UnresolvedPacket>
    {
        private readonly ILogger logger;
        private readonly AccountRepository accountRepository;
        private readonly CharacterRepository characterRepository;

        private readonly Dictionary<Guid, string> storedUsernames = new();
        
        public UnresolvedProcessor(ILogger logger, AccountRepository accountRepository, CharacterRepository characterRepository)
        {
            this.logger = logger;
            this.accountRepository = accountRepository;
            this.characterRepository = characterRepository;
        }

        protected override async Task Process(WorldSession session, UnresolvedPacket packet)
        {
            if (session.Key == 0)
            {
                session.Key = int.Parse(packet.Header);
                return;
            }

            string username = storedUsernames.GetValueOrDefault(session.Id);
            if (username == null)
            {
                storedUsernames[session.Id] = packet.Header;
                return;
            }

            if (session.Account == null)
            {
                AccountDTO accountDto = await accountRepository.GetAccountByName(username);
                if (accountDto == null)
                {
                    logger.Debug("Can't found account");
                    await session.Disconnect();
                    return;
                }

                if (!string.Equals(accountDto.Password, packet.Header.ToSha512(), StringComparison.CurrentCultureIgnoreCase))
                {
                    logger.Debug("Wrong password");
                    await session.Disconnect();
                    return;
                }
                
                storedUsernames.Remove(session.Id);

                session.Account = accountDto;

                IEnumerable<CharacterDTO> characters = await characterRepository.FindAll(accountDto.Id);

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
}