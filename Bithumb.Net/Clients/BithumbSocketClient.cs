using Bithumb.Net.Clients.WebSocketApis;
using Bithumb.Net.Interfaces;

using System.Net.WebSockets;

namespace Bithumb.Net.Clients
{
    public class BithumbSocketClient : ISocketClient
    {
        public ClientWebSocket SocketClient { get; init; }
        public BithumbWebSocketApi Streams { get; set; }

        public BithumbSocketClient()
        {
            SocketClient = new ClientWebSocket();
            Streams = new BithumbWebSocketApi(SocketClient);
        }
    }
}
