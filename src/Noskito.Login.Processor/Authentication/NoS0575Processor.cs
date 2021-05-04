﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Noskito.Common.Logging;
using Noskito.Communication.Abstraction.Server;
using Noskito.Database.Abstraction.Repository;
using Noskito.Enum.Authentication;
using Noskito.Login.Abstraction.Network;
using Noskito.Login.Packet.Client.Authentication;
using Noskito.Login.Packet.Server.Authentication;
using Noskito.Login.Processor.Extension;

namespace Noskito.Login.Processor.Authentication
{
    public class NoS0575Processor : PacketProcessor<NoS0575>
    {
        private readonly ILogger logger;
        private readonly IAccountRepository accountRepository;
        private readonly IServerService serverService;

        public NoS0575Processor(ILogger logger, IAccountRepository accountRepository, IServerService serverService)
        {
            this.logger = logger;
            this.accountRepository = accountRepository;
            this.serverService = serverService;
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

            var maintenance = await serverService.IsMaintenanceMode();
            if (maintenance)
            {
                await client.SendLoginFail(LoginFailReason.Maintenance);
                await client.Disconnect();
                return;
            }

            var worlds = await serverService.GetWorldServers();
            if (!worlds.Any())
            {
                await client.SendLoginFail(LoginFailReason.CantConnect);
                await client.Disconnect();
                return;
            }

            var grouped = worlds.GroupBy(x => x.Name).ToArray();
            
            var convertedServers = new List<NsTeST.Server>();
            for (var i = 0; i < grouped.Length; i++)
            {
                var servers = grouped[0].ToArray();
                for (var j = 0; j < servers.Length; j++)
                {
                    var server = servers[j];
                    convertedServers.Add(new NsTeST.Server
                    {
                        Host = server.Host,
                        Port = server.Port,
                        Name = server.Name,
                        Color = 0,
                        Id = i,
                        Count = j
                    });
                }
            }
            await client.SendPacket(new NsTeST
            {
                RegionId = 0,
                EncryptionKey = 15000,
                Account = account.Username,
                Servers = convertedServers
            });
            await client.Disconnect();
        }
    }
}