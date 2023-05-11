using Bithumb.Net.Converters;
using Bithumb.Net.Enums;
using Bithumb.Net.Extensions;
using Bithumb.Net.Objects;
using Bithumb.Net.Objects.Models;
using Bithumb.Net.Objects.Models.ResponseModels;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System.Net.WebSockets;
using System.Text;

namespace Bithumb.Net.Clients.WebSocketApis
{
    public class BithumbWebSocketApi
    {
        ClientWebSocket socketClient = default!;
        bool isTickerSubscribe = false;
        bool isTransactionSubscribe = false;
        bool isOrderbookDepthSubscribe = false;
        bool isOrderbookSnapshotSubscribe = false;

        public BithumbWebSocketApi(ClientWebSocket socketClient)
        {
            this.socketClient = socketClient;
            Connect().Wait();
        }

        private async Task Connect()
        {
            await socketClient.ConnectAsync(new Uri(BithumbUrls.WebSocketAddress), CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// WebSocket을 이용한 방식으로, 현재가에 대한 정보를 수신할 수 있습니다.
        /// </summary>
        /// <param name="symbols">BTC_KRW, ETH_KRW, …</param>
        /// <param name="ticks">tick 종류 ("30M"/"1H"/"12H"/"24H"/"MID")</param>
        /// <param name="onMessage">실시간 메시지를 받을 메서드</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <seealso cref="https://apidocs.bithumb.com/reference/%EB%B9%97%EC%8D%B8-%EA%B1%B0%EB%9E%98%EC%86%8C-%EC%A0%95%EB%B3%B4-%EC%88%98%EC%8B%A0"/>
        public async Task<string> SubscribeToTickerAsync(string symbol, BithumbSocketTickInterval tick, Action<BithumbWebSocketResponse<BithumbWebSocketTicker>> onMessage, CancellationToken cancellationToken = default)
        {
            return await SubscribeToTickerAsync(new string[] { symbol }, new BithumbSocketTickInterval[] { tick }, onMessage, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// WebSocket을 이용한 방식으로, 현재가에 대한 정보를 수신할 수 있습니다.
        /// </summary>
        /// <param name="symbols">BTC_KRW, ETH_KRW, …</param>
        /// <param name="ticks">tick 종류 ("30M"/"1H"/"12H"/"24H"/"MID")</param>
        /// <param name="onMessage">실시간 메시지를 받을 메서드</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <seealso cref="https://apidocs.bithumb.com/reference/%EB%B9%97%EC%8D%B8-%EA%B1%B0%EB%9E%98%EC%86%8C-%EC%A0%95%EB%B3%B4-%EC%88%98%EC%8B%A0"/>
        public async Task<string> SubscribeToTickerAsync(string symbol, IEnumerable<BithumbSocketTickInterval> ticks, Action<BithumbWebSocketResponse<BithumbWebSocketTicker>> onMessage, CancellationToken cancellationToken = default)
        {
            return await SubscribeToTickerAsync(new string[] { symbol }, ticks, onMessage, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// WebSocket을 이용한 방식으로, 현재가에 대한 정보를 수신할 수 있습니다.
        /// </summary>
        /// <param name="symbols">BTC_KRW, ETH_KRW, …</param>
        /// <param name="ticks">tick 종류 ("30M"/"1H"/"12H"/"24H"/"MID")</param>
        /// <param name="onMessage">실시간 메시지를 받을 메서드</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <seealso cref="https://apidocs.bithumb.com/reference/%EB%B9%97%EC%8D%B8-%EA%B1%B0%EB%9E%98%EC%86%8C-%EC%A0%95%EB%B3%B4-%EC%88%98%EC%8B%A0"/>
        public async Task<string> SubscribeToTickerAsync(IEnumerable<string> symbols, BithumbSocketTickInterval tick, Action<BithumbWebSocketResponse<BithumbWebSocketTicker>> onMessage, CancellationToken cancellationToken = default)
        {
            return await SubscribeToTickerAsync(symbols, new BithumbSocketTickInterval[] { tick }, onMessage, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// WebSocket을 이용한 방식으로, 현재가에 대한 정보를 수신할 수 있습니다.
        /// </summary>
        /// <param name="symbols">BTC_KRW, ETH_KRW, …</param>
        /// <param name="ticks">tick 종류 ("30M"/"1H"/"12H"/"24H"/"MID")</param>
        /// <param name="onMessage">실시간 메시지를 받을 메서드</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <seealso cref="https://apidocs.bithumb.com/reference/%EB%B9%97%EC%8D%B8-%EA%B1%B0%EB%9E%98%EC%86%8C-%EC%A0%95%EB%B3%B4-%EC%88%98%EC%8B%A0"/>
        public async Task<string> SubscribeToTickerAsync(IEnumerable<string> symbols, IEnumerable<BithumbSocketTickInterval> ticks, Action<BithumbWebSocketResponse<BithumbWebSocketTicker>> onMessage, CancellationToken cancellationToken = default)
        {
            if (socketClient.State != WebSocketState.Open)
            {
                return "Socket is not open.";
            }

            var handler = new Action<BithumbWebSocketResponse<BithumbWebSocketTicker>>(data => onMessage(data));

            var subscriptionParameter = new JObject(
                new JProperty("type", "ticker"),
                new JProperty("symbols", new JArray(symbols)),
                new JProperty("tickTypes", new JArray(ticks.Select(t => t.EnumToString())))
                );

            await socketClient.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(subscriptionParameter.ToString())), WebSocketMessageType.Text, true, cancellationToken).ConfigureAwait(false);
            isTickerSubscribe = true;

            while (socketClient.State == WebSocketState.Open && isTickerSubscribe)
            {
                var buffer = new byte[1024];
                var data = await socketClient.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationToken).ConfigureAwait(false);

                if (data.MessageType == WebSocketMessageType.Text)
                {
                    var messageBytes = new byte[data.Count];
                    Array.Copy(buffer, 0, messageBytes, 0, data.Count);
                    var messageJson = Encoding.UTF8.GetString(messageBytes);

                    if (messageJson.Contains("resmsg"))
                    {
                        // Not supported: Does not return
                        var connectionResult = JsonConvert.DeserializeObject<BithumbWebSocketConnection>(messageJson) ?? default!;
                    }
                    else
                    {
                        var result = JsonConvert.DeserializeObject<BithumbWebSocketResponse<BithumbWebSocketTicker>>(messageJson, new BithumbWebSocketTickerConverter()) ?? default!;
                        handler.Invoke(result);
                    }
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// 현재가에 대한 정보 수신을 중단합니다.
        /// </summary>
        public void UnsubscribeToTicker()
        {
            isTickerSubscribe = false;
        }

        /// <summary>
        /// WebSocket을 이용한 방식으로, 체결에 대한 정보를 수신할 수 있습니다.
        /// </summary>
        /// <param name="symbol">BTC_KRW, ETH_KRW, …</param>
        /// <param name="onMessage">실시간 메시지를 받을 메서드</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <seealso cref="https://apidocs.bithumb.com/reference/%EB%B9%97%EC%8D%B8-%EA%B1%B0%EB%9E%98%EC%86%8C-%EC%A0%95%EB%B3%B4-%EC%88%98%EC%8B%A0"/>
        public async Task<string> SubscribeToTransactionAsync(string symbol, Action<BithumbWebSocketResponse<BithumbWebSocketTransactions>> onMessage, CancellationToken cancellationToken = default)
        {
            return await SubscribeToTransactionAsync(new string[] { symbol }, onMessage, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// WebSocket을 이용한 방식으로, 체결에 대한 정보를 수신할 수 있습니다.
        /// </summary>
        /// <param name="symbol">BTC_KRW, ETH_KRW, …</param>
        /// <param name="onMessage">실시간 메시지를 받을 메서드</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <seealso cref="https://apidocs.bithumb.com/reference/%EB%B9%97%EC%8D%B8-%EA%B1%B0%EB%9E%98%EC%86%8C-%EC%A0%95%EB%B3%B4-%EC%88%98%EC%8B%A0"/>
        public async Task<string> SubscribeToTransactionAsync(IEnumerable<string> symbols, Action<BithumbWebSocketResponse<BithumbWebSocketTransactions>> onMessage, CancellationToken cancellationToken = default)
        {
            if (socketClient.State != WebSocketState.Open)
            {
                return "Socket is not open.";
            }

            var handler = new Action<BithumbWebSocketResponse<BithumbWebSocketTransactions>>(data => onMessage(data));

            var subscriptionParameter = new JObject(
                new JProperty("type", "transaction"),
                new JProperty("symbols", new JArray(symbols))
                );

            await socketClient.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(subscriptionParameter.ToString())), WebSocketMessageType.Text, true, cancellationToken).ConfigureAwait(false);
            isTransactionSubscribe = true;

            while (socketClient.State == WebSocketState.Open && isTransactionSubscribe)
            {
                var buffer = new byte[1024];
                var data = await socketClient.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationToken).ConfigureAwait(false);

                if (data.MessageType == WebSocketMessageType.Text)
                {
                    var messageBytes = new byte[data.Count];
                    Array.Copy(buffer, 0, messageBytes, 0, data.Count);
                    var messageJson = Encoding.UTF8.GetString(messageBytes);

                    if (messageJson.Contains("resmsg"))
                    {
                        // Not supported: Does not return
                        var connectionResult = JsonConvert.DeserializeObject<BithumbWebSocketConnection>(messageJson) ?? default!;
                    }
                    else
                    {
                        var result = JsonConvert.DeserializeObject<BithumbWebSocketResponse<BithumbWebSocketTransactions>>(messageJson) ?? default!;
                        handler.Invoke(result);
                    }
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// 체결에 대한 정보 수신을 중단합니다.
        /// </summary>
        public void UnsubscribeToTransaction()
        {
            isTransactionSubscribe = false;
        }

        /// <summary>
        /// WebSocket을 이용한 방식으로, 호가에 대한 정보를 수신할 수 있습니다.
        /// </summary>
        /// <param name="symbol">BTC_KRW, ETH_KRW, …</param>
        /// <param name="onMessage">실시간 메시지를 받을 메서드</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <seealso cref="https://apidocs.bithumb.com/reference/%EB%B9%97%EC%8D%B8-%EA%B1%B0%EB%9E%98%EC%86%8C-%EC%A0%95%EB%B3%B4-%EC%88%98%EC%8B%A0"/>
        public async Task<string> SubscribeToOrderbookDepthAsync(string symbol, Action<BithumbWebSocketResponse<BithumbWebSocketOrderbookDepths>> onMessage, CancellationToken cancellationToken = default)
        {
            return await SubscribeToOrderbookDepthAsync(new string[] { symbol }, onMessage, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// WebSocket을 이용한 방식으로, 호가에 대한 정보를 수신할 수 있습니다.
        /// </summary>
        /// <param name="symbol">BTC_KRW, ETH_KRW, …</param>
        /// <param name="onMessage">실시간 메시지를 받을 메서드</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <seealso cref="https://apidocs.bithumb.com/reference/%EB%B9%97%EC%8D%B8-%EA%B1%B0%EB%9E%98%EC%86%8C-%EC%A0%95%EB%B3%B4-%EC%88%98%EC%8B%A0"/>
        public async Task<string> SubscribeToOrderbookDepthAsync(IEnumerable<string> symbols, Action<BithumbWebSocketResponse<BithumbWebSocketOrderbookDepths>> onMessage, CancellationToken cancellationToken = default)
        {
            if (socketClient.State != WebSocketState.Open)
            {
                return "Socket is not open.";
            }

            var handler = new Action<BithumbWebSocketResponse<BithumbWebSocketOrderbookDepths>>(data => onMessage(data));

            var subscriptionParameter = new JObject(
                new JProperty("type", "orderbookdepth"),
                new JProperty("symbols", new JArray(symbols))
                );

            await socketClient.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(subscriptionParameter.ToString())), WebSocketMessageType.Text, true, cancellationToken).ConfigureAwait(false);
            isOrderbookDepthSubscribe = true;

            while (socketClient.State == WebSocketState.Open && isOrderbookDepthSubscribe)
            {
                var buffer = new byte[1024];
                var data = await socketClient.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationToken).ConfigureAwait(false);

                if (data.MessageType == WebSocketMessageType.Text)
                {
                    var messageBytes = new byte[data.Count];
                    Array.Copy(buffer, 0, messageBytes, 0, data.Count);
                    var messageJson = Encoding.UTF8.GetString(messageBytes);

                    if (messageJson.Contains("resmsg"))
                    {
                        // Not supported: Does not return
                        var connectionResult = JsonConvert.DeserializeObject<BithumbWebSocketConnection>(messageJson) ?? default!;
                    }
                    else
                    {
                        var result = JsonConvert.DeserializeObject<BithumbWebSocketResponse<BithumbWebSocketOrderbookDepths>>(messageJson, new BithumbWebSocketOrderbookDepthsConverter()) ?? default!;
                        handler.Invoke(result);
                    }
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// 호가에 대한 정보 수신을 중단합니다.
        /// </summary>
        public void UnsubscribeToOrderbookDepth()
        {
            isOrderbookDepthSubscribe = false;
        }

        /// <summary>
        /// WebSocket을 이용한 방식으로, 호가에 대한 정보를 수신할 수 있습니다.
        /// </summary>
        /// <param name="symbol">BTC_KRW, ETH_KRW, …</param>
        /// <param name="onMessage">실시간 메시지를 받을 메서드</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <seealso cref="https://apidocs.bithumb.com/reference/%EB%B9%97%EC%8D%B8-%EA%B1%B0%EB%9E%98%EC%86%8C-%EC%A0%95%EB%B3%B4-%EC%88%98%EC%8B%A0"/>
        public async Task<string> SubscribeToOrderbookSnapshotAsync(string symbol, Action<BithumbWebSocketResponse<BithumbWebSocketOrderbookSnapshot>> onMessage, CancellationToken cancellationToken = default)
        {
            return await SubscribeToOrderbookSnapshotAsync(new string[] { symbol }, onMessage, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// WebSocket을 이용한 방식으로, 호가에 대한 정보를 수신할 수 있습니다.
        /// </summary>
        /// <param name="symbol">BTC_KRW, ETH_KRW, …</param>
        /// <param name="onMessage">실시간 메시지를 받을 메서드</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <seealso cref="https://apidocs.bithumb.com/reference/%EB%B9%97%EC%8D%B8-%EA%B1%B0%EB%9E%98%EC%86%8C-%EC%A0%95%EB%B3%B4-%EC%88%98%EC%8B%A0"/>
        public async Task<string> SubscribeToOrderbookSnapshotAsync(IEnumerable<string> symbols, Action<BithumbWebSocketResponse<BithumbWebSocketOrderbookSnapshot>> onMessage, CancellationToken cancellationToken = default)
        {
            if (socketClient.State != WebSocketState.Open)
            {
                return "Socket is not open.";
            }

            var handler = new Action<BithumbWebSocketResponse<BithumbWebSocketOrderbookSnapshot>>(data => onMessage(data));

            var subscriptionParameter = new JObject(
                new JProperty("type", "orderbooksnapshot"),
                new JProperty("symbols", new JArray(symbols))
                );

            await socketClient.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(subscriptionParameter.ToString())), WebSocketMessageType.Text, true, cancellationToken).ConfigureAwait(false);
            isOrderbookSnapshotSubscribe = true;

            while (socketClient.State == WebSocketState.Open && isOrderbookSnapshotSubscribe)
            {
                var buffer = new byte[2048];
                var data = await socketClient.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationToken).ConfigureAwait(false);

                if (data.MessageType == WebSocketMessageType.Text)
                {
                    var messageBytes = new byte[data.Count];
                    Array.Copy(buffer, 0, messageBytes, 0, data.Count);
                    var messageJson = Encoding.UTF8.GetString(messageBytes);

                    if (messageJson.Contains("resmsg"))
                    {
                        // Not supported: Does not return
                        var connectionResult = JsonConvert.DeserializeObject<BithumbWebSocketConnection>(messageJson) ?? default!;
                    }
                    else
                    {
                        var result = JsonConvert.DeserializeObject<BithumbWebSocketResponse<BithumbWebSocketOrderbookSnapshot>>(messageJson, new BithumbWebSocketOrderbookSnapshotConverter()) ?? default!;
                        handler.Invoke(result);
                    }
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// 호가에 대한 정보 수신을 중단합니다.
        /// </summary>
        public void UnsubscribeToOrderbookSnapshot()
        {
            isOrderbookSnapshotSubscribe = false;
        }
    }
}
