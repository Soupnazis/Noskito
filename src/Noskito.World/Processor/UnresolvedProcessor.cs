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

        public UnresolvedProcessor(ILogger logger, AccountRepository accountRepository, CharacterRepository characterRepository)
        {
            this.logger = logger;
            this.accountRepository = accountRepository;
            this.characterRepository = characterRepository;
        }

        protected override async Task Process(WorldSession session, UnresolvedPacket packet)
        {
            if (session.Client.EncryptionKey == 0)
            {
                session.Client.EncryptionKey = int.Parse(packet.Header);
                return;
            }

            if (session.Username == null)
            {
                session.Username = packet.Header;
                return;
            }

            if (session.Account == null)
            {
                AccountDTO accountDto = await accountRepository.GetAccountByName(session.Username);
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