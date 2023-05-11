using System.Net.WebSockets;

namespace Bithumb.Net.Interfaces
{
    public interface ISocketClient
    {
        ClientWebSocket SocketClient { get; }
    }
}
