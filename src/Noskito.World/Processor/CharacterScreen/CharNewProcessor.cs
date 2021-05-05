using System.Linq;
using System.Threading.Tasks;
using Noskito.Database.Dto;
using Noskito.Database.Repository;
using Noskito.Enum;
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
            if (session.Account is null) return;

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
                Level = 1,
                JobLevel = 1,
                Job = Job.Adventurer,
                Gender = packet.Gender,
                HairColor = packet.HairColor,
                HairStyle = packet.HairStyle,
                MapId = 1,
                X = 75,
                Y = 115
            });

            await session.SendPacket(new Success());

            var characters = await characterRepository.FindAll(session.Account.Id);

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