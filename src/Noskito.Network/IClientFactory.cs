using DotNetty.Transport.Channels;

namespace Noskito.Network
{
    public interface IClientFactory
    {
        Client CreateClient(IChannel channel);
    }
}