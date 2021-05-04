namespace Noskito.Database.Abstraction.Entity
{
    public record Account
    {
        public long Id { get; init; }
        public string Username { get; init; }
        public string Password { get; init; }
    }
}