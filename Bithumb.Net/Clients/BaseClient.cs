using Bithumb.Net.Extensions;
using Bithumb.Net.Interfaces;
using Bithumb.Net.Objects;

using Newtonsoft.Json;

using System.Security.Cryptography;
using System.Text;

namespace Bithumb.Net.Clients
{
    public class BaseClient : IClient
    {
        protected string connectKey { get; private set; } = string.Empty;
        protected string secretKey { get; private set; } = string.Empty;

        public HttpClient Client { get; init; }

        public BaseClient()
        {
            Client = new HttpClient();
        }

        public BaseClient(HttpClient client, string connectKey, string secretKey)
        {
            Client = client;
            this.connectKey = connectKey;
            this.secretKey = secretKey;
        }

        ~BaseClient()
        {
            Client.Dispose();
        }

        /// <summary>
        /// Get request for Bithumb
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="client"></param>
        /// <param name="endpoint">/info/account</param>
        /// <param name="parameters">{"order_currency", "BTC"}, ...</param>
        /// <returns></returns>
        protected async Task<T> GetBithumbAsync<T>(HttpClient client, string endpoint, IDictionary<string, string>? parameters = null, JsonConverter? converter = null)
        {
            try
            {
                var url = parameters == null ? BithumbUrls.OpenApiHost + endpoint : BithumbUrls.OpenApiHost + endpoint + "?" + string.Join('&', parameters.Select(p => p.Key + "=" + p.Value).ToArray());

                var response = await client.GetAsync(url).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    if (converter == null)
                    {
                        return JsonConvert.DeserializeObject<T>(result) ?? default!;
                    }
                    return JsonConvert.DeserializeObject<T>(result, converter) ?? default!;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Post request with authorization for Bithumb 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="client"></param>
        /// <param name="endpoint">/info/account</param>
        /// <param name="parameters">{"order_currency", "BTC"}, ...</param>
        /// <returns></returns>
        /// <seealso cref="https://apidocs.bithumb.com/docs/%EC%9D%B8%EC%A6%9D-%ED%97%A4%EB%8D%94-%EB%A7%8C%EB%93%A4%EA%B8%B0"/>
        protected async Task<T> PostBithumbAuthorizationAsync<T>(HttpClient client, string endpoint, IDictionary<string, string>? parameters = null, JsonConverter? converter = null)
        {
            if (parameters == null)
            {
                parameters = new Dictionary<string, string>
                {
                    { "endpoint", endpoint }
                };
            }
            else
            {
                parameters.InsertAtBeginning("endpoint", endpoint);
            }

            var queryString = string.Join('&', parameters.Select(p => p.Key + "=" + p.Value).ToArray());
            var encodedQueryString = queryString.Replace("/", "%2F");
            var nonce = ((long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds).ToString();
            var message = $"{endpoint}\0{encodedQueryString}\0{nonce}";

            var hash = Array.Empty<byte>();
            using (var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(secretKey)))
            {
                hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
            }
            var hex = BitConverter.ToString(hash).Replace("-", "").ToLower();
            var sign = Convert.ToBase64String(Encoding.UTF8.GetBytes(hex));

            var headers = new Dictionary<string, string>
            {
                { "Api-Key", connectKey },
                { "Api-Nonce", nonce },
                { "Api-Sign", sign }
            };

            var content = new FormUrlEncodedContent(parameters);
            foreach (var header in headers)
            {
                client.DefaultRequestHeaders.Add(header.Key, header.Value);
            }

            var url = BithumbUrls.OpenApiHost + endpoint;
            var response = await client.PostAsync(url, content).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (converter == null)
                {
                    return JsonConvert.DeserializeObject<T>(result) ?? default!;
                }
                return JsonConvert.DeserializeObject<T>(result, converter) ?? default!;
            }
            else
            {
                return default!;
            }
        }
    }
}
