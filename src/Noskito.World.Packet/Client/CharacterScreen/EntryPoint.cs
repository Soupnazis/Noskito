namespace Noskito.World.Packet.Client.CharacterScreen
{
    /// <summary>
    /// Special packet
    /// </summary>
    public class EntryPoint : CPacket
    {
        public string Password { get; init; }
    }
}