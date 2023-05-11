using Bithumb.Net.Objects.Models;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Bithumb.Net.Converters
{
    public class BithumbOrderbooksConverter : JsonConverter<BithumbOrderbooks>
    {
        public override BithumbOrderbooks? ReadJson(JsonReader reader, Type objectType, BithumbOrderbooks? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);
            var properties = jsonObject.Properties();

            long timestamp = 0;
            var payment_currency = string.Empty;
            var orderbooks = new List<BithumbOrderbook>();
            foreach (var orderbook in properties)
            {
                switch (orderbook.Name)
                {
                    case "timestamp":
                        timestamp = long.Parse(orderbook.Value.ToString());
                        break;

                    case "payment_currency":
                        payment_currency = orderbook.Value.ToString();
                        break;

                    default:
                        var _orderbook = JsonConvert.DeserializeObject<BithumbOrderbook>(orderbook.Value.ToString()) ?? default!;
                        orderbooks.Add(_orderbook);
                        break;
                }
            }

            return new BithumbOrderbooks(timestamp, payment_currency, orderbooks);
        }

        public override void WriteJson(JsonWriter writer, BithumbOrderbooks? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
