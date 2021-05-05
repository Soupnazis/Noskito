namespace Noskito.World.Packet.Server.Player
{
    public class Lev : SPacket
    {
        public int Level { get; init; }
        public long Experience { get; init; }
        public long RequiredExperience { get; init; }

        public int JobLevel { get; init; }
        public long JobExperience { get; init; }
        public long RequiredJobExperience { get; init; }

        public int HeroLevel { get; init; }
        public long HeroExperience { get; init; }
        public long RequiredHeroExperience { get; init; }

        public long Reputation { get; init; }
        public int Cp { get; init; }
    }

    public class LevCreator : SPacketCreator<Lev>
    {
        protected override string CreatePacket(Lev source)
        {
            return "lev " +
                   $"{source.Level} " +
                   $"{source.Experience} " +
                   $"{source.JobLevel} " +
                   $"{source.JobExperience} " +
                   $"{source.RequiredExperience} " +
                   $"{source.RequiredJobExperience} " +
                   $"{source.Reputation} " +
                   $"{source.Cp} " +
                   $"{source.HeroExperience} " +
                   $"{source.HeroLevel} " +
                   $"{source.RequiredHeroExperience}";
        }
    }
}