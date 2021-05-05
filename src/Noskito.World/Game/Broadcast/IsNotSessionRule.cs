namespace Noskito.World.Game.Broadcast
{
    public class IsNotSessionRule : IBroadcastRule
    {
        private readonly WorldSession target;

        public IsNotSessionRule(WorldSession target)
        {
            this.target = target;
        }

        public bool Match(WorldSession session)
        {
            return target.Id != session.Id;
        }
    }
}