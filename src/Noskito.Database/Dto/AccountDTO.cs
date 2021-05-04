namespace Noskito.Database.Dto
{
    public record AccountDTO
    {
        public long Id { get; init; }
        public string Username { get; init; }
        public string Password { get; init; }
    }
}