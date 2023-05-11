using Bithumb.Net.Clients.CandlestickApis;
using Bithumb.Net.Clients.PrivateApis;
using Bithumb.Net.Clients.PublicApis;

namespace Bithumb.Net.Clients
{
    public class BithumbClient : BaseClient
    {
        public BithumbPublicApi Public { get; set; }
        public BithumbInfoApi Info { get; set; }
        public BithumbTradeApi Trade { get; set; }
        public BithumbCandlestickApi Candlestick { get; set; }

        public BithumbClient() : this(string.Empty, string.Empty)
        {

        }

        public BithumbClient(string connectKey, string secretKey)
        {
            Client = new HttpClient(new HttpClientHandler() { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true });

            Public = new BithumbPublicApi(Client);
            Info = new BithumbInfoApi(Client, connectKey, secretKey);
            Trade = new BithumbTradeApi(Client, connectKey, secretKey);
            Candlestick = new BithumbCandlestickApi(Client);
        }
    }
}
