using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Noskito.Common.Logging;
using Noskito.Database.Abstraction.Repository;
using Noskito.Enum.Authentication;
using Noskito.Login.Abstraction.Network;
using Noskito.Login.Processor.Extension;
using Noskito.Packet.Client.Authentication;
using Noskito.Packet.Server.Authentication;

namespace Noskito.Login.Processor.Authentication
{
    public class NoS0575Processor : PacketProcessor<NoS0575>
    {
        private readonly ILogger logger;
        private readonly IAccountRepository accountRepository;

        public NoS0575Processor(ILogger logger, IAccountRepository accountRepository)
        {
            this.logger = logger;
            this.accountRepository = accountRepository;
        }

        protected override async Task Process(ILoginClient client, NoS0575 packet)
        {
            var account = await accountRepository.GetAccountByName(packet.Username);
            if (account == null)
            {
                await client.SendLoginFail(LoginFailReason.AccountOrPasswordWrong);
                await client.Disconnect();
                return;
            }

            if (!string.Equals(account.Password, packet.Password, StringComparison.CurrentCultureIgnoreCase))
            {
                await client.SendLoginFail(LoginFailReason.AccountOrPasswordWrong);
                await client.Disconnect();
                return;
            }

            var servers = new List<NsTeSt.Server>
            {
                new()
                {
                    Host = "localhost",
                    Port = 5555,
                    Name = "Noskito",
                    Color = 0,
                    Count = 0,
                    Id = 0
                }
            };
            
            await client.SendPacket(new NsTeSt
            {
                RegionId = 0,
                SessionId = 1,
                Account = account.Username,
                Servers = servers
            });
            await client.Disconnect();
        }
    }
}