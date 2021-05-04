using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Noskito.Common.Extension;
using Noskito.Common.Logging;
using Noskito.Database.Abstraction.Entity;
using Noskito.Database.Abstraction.Repository;
using Noskito.World.Abstraction.Network;
using Noskito.World.Packet.Client.CharacterScreen;
using Noskito.World.Packet.Server.CharacterScreen;

namespace Noskito.World.Processor.CharacterScreen
{
    public class EntryPointProcessor : PacketProcessor<EntryPoint>
    {
        private readonly ILogger logger;
        private readonly IAccountRepository accountRepository;
        private readonly ICharacterRepository characterRepository;

        public EntryPointProcessor(ILogger logger, IAccountRepository accountRepository, ICharacterRepository characterRepository)
        {
            this.logger = logger;
            this.accountRepository = accountRepository;
            this.characterRepository = characterRepository;
        }

        protected override async Task Process(IWorldClient client, EntryPoint packet)
        {
            if (client.Account == null)
            {
                if (packet.Password == null)
                {
                    await client.Disconnect();
                    return;
                }
                
                string password = packet.Password;
            
                var account = await accountRepository.GetAccountByName(client.Username);
                if (account == null)
                {
                    logger.Debug($"Can't found account {client.Username}");
                    await client.Disconnect();
                    return;
                }

                if (!string.Equals(account.Password, password.ToSha512(), StringComparison.CurrentCultureIgnoreCase))
                {
                    logger.Debug($"Wrong password for account {account.Username}");
                    await client.Disconnect();
                    return;
                }

                client.Account = account;
            }

            if (client.Account == null)
            {
                await client.Disconnect();
                return;
            }
            
            IEnumerable<Character> characters = await characterRepository.FindAll(client.Account.Id);

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