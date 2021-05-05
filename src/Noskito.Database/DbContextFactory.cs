namespace Noskito.Database
{
    public class DbContextFactory
    {
        public NoskitoContext CreateContext()
        {
            return new();
        }
    }
}