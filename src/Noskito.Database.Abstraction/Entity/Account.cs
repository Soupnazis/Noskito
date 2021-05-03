namespace Noskito.Database.Abstraction.Entities
{
    public record Account
    {
        public string Username { get; init; }
        public string Password { get; init; }
    }
}