namespace Noskito.World.Game.Broadcast
{
    public interface IBroadcastRule
    {
        bool Match(WorldSession session);
    }
}