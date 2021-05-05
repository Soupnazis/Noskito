using System.Threading.Tasks;
using Noskito.Enum;
using Noskito.World.Game.Entities;
using Noskito.World.Game.Maps;
using Noskito.World.Packet.Server.Maps;
using Noskito.World.Packet.Server.Player;

namespace Noskito.World.Processor.Extension
{
    public static class PacketSendingExtensions
    {
        public static Task SendTit(this WorldSession session, int classId, string name)
        {
            return session.SendPacket(new Tit
            {
                ClassName = classId,
                Name = name
            });
        }

        public static Task SendFd(this WorldSession session, long reputation, int reputationIcon, int dignity,
            int dignityIcon)
        {
            return session.SendPacket(new Fd
            {
                Reputation = reputation,
                RepucationIcon = reputationIcon,
                Dignity = dignity,
                DignityIcon = dignityIcon
            });
        }

        public static Task SendCInfo(this WorldSession session, Character character)
        {
            return session.SendPacket(new CInfo
            {
                Name = character.Name,
                CharacterId = character.Id,
                AuthorityType = AuthorityType.GameMaster,
                Gender = character.Gender,
                HairStyle = character.HairStyle,
                HairColor = character.HairColor,
                Job = character.Job,
                Icon = 1,
                Compliment = 0,
                Morph = 0,
                Invisible = false,
                FamilyLevel = 0,
                MorphUpgrade = 0,
                ArenaWinner = false
            });
        }

        public static Task SendLev(this WorldSession session, Character character)
        {
            return session.SendPacket(new Lev
            {
                Level = character.Level,
                Experience = 0,
                RequiredExperience = 1,
                JobLevel = character.JobLevel,
                JobExperience = 0,
                RequiredJobExperience = 1,
                HeroLevel = character.HeroLevel,
                HeroExperience = 0,
                RequiredHeroExperience = 1,
                Reputation = 0,
                Cp = 0
            });
        }

        public static Task SendAt(this WorldSession session, Character character)
        {
            return session.SendPacket(new At
            {
                CharacterId = character.Id,
                MapId = character.Map.Id,
                X = character.Position.X,
                Y = character.Position.Y,
                Music = 1,
                Direction = 0
            });
        }

        public static Task SendCMap(this WorldSession session, Map map, bool joining = true)
        {
            return session.SendPacket(new CMap
            {
                MapId = map.Id,
                IsJoining = joining
            });
        }
    }
}