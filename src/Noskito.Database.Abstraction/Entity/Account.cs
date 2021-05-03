namespace Noskito.Database.Abstraction.Entity
{
    public record Account
    {
        public string Username { get; init; }
        public string Password { get; init; }
    }
}