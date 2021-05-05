using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Noskito.Common.Extension;
using Noskito.Common.Logging;
using Noskito.Database.Repository;
using Noskito.World.Packet.Client;
using Noskito.World.Packet.Server.CharacterScreen;

namespace Noskito.World.Processor
{
    public class UnresolvedProcessor : PacketProcessor<UnresolvedPacket>
    {
        private readonly AccountRepository accountRepository;
        private readonly CharacterRepository characterRepository;
        private readonly ILogger logger;

        private readonly Dictionary<Guid, string> storedUsernames = new();

        public UnresolvedProcessor(ILogger logger, AccountRepository accountRepository,
            CharacterRepository characterRepository)
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

            var username = storedUsernames.GetValueOrDefault(session.Id);
            if (username is null)
            {
                storedUsernames[session.Id] = packet.Header;
                return;
            }

            if (session.Account is null)
            {
                var accountDto = await accountRepository.GetAccountByName(username);
                if (accountDto == null)
                {
                    logger.Debug("Can't found account");
                    await session.Disconnect();
                    return;
                }

                if (!string.Equals(accountDto.Password, packet.Header.ToSha512(),
                    StringComparison.CurrentCultureIgnoreCase))
                {
                    logger.Debug("Wrong password");
                    await session.Disconnect();
                    return;
                }

                storedUsernames.Remove(session.Id);

                session.Account = accountDto;

                var characters = await characterRepository.FindAll(accountDto.Id);

                await session.SendPacket(new CListStart());
                foreach (var character in characters)
                    await session.SendPacket(new CList
                    {
                        Name = character.Name,
                        Slot = character.Slot,
                        HairColor = character.HairColor,
                        HairStyle = character.HairStyle,
                        Level = character.Level,
                        Gender = character.Gender,
                        HeroLevel = 0,
                        JobLevel = character.JobLevel,
                        Job = character.Job,
                        Equipments = Enumerable.Range(0, 10).Select(x => (short?) null),
                        Pets = Enumerable.Range(0, 24).Select(x => (short?) null),
                        QuestCompletion = 1,
                        QuestPart = 1,
                        Rename = false
                    });
                await session.SendPacket(new CListEnd());
            }
        }
    }
}