namespace Noskito.Database
{
    public class ContextFactory
    {
        public NoskitoContext CreateContext()
        {
            return new NoskitoContext();
        }
    }
}