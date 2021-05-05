namespace Noskito.World.Packet.Client.CharacterScreen
{
    public class GameStart : CPacket
    {
    }

    public class GameStartCreator : CPacketCreator<GameStart>
    {
        public override string Header { get; } = "game_start";

        protected override GameStart CreatePacket(string[] parameters)
        {
            return new();
        }
    }
}