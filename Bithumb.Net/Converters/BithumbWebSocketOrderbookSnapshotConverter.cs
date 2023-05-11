using Bithumb.Net.Extensions;
using Bithumb.Net.Objects.Models;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Bithumb.Net.Converters
{
    public class BithumbWebSocketOrderbookSnapshotConverter : JsonConverter<BithumbWebSocketOrderbookSnapshot>
    {
        public override BithumbWebSocketOrderbookSnapshot? ReadJson(JsonReader reader, Type objectType, BithumbWebSocketOrderbookSnapshot? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);
            var properties = jsonObject.Properties();

            var symbol = properties.GetString("symbol");
            var datetime = (long.Parse(properties.GetString("datetime")) / 1000).ToDateTime();
            var asks = new List<BithumbWebSocketOrderbookSnapshotValue>();
            var bids = new List<BithumbWebSocketOrderbookSnapshotValue>();

            var asksData = (JsonConvert.DeserializeObject<IEnumerable<object>>(properties.GetString("asks")) ?? default!).Cast<JArray>();
            foreach(var obj in asksData)
            {
                var price = obj[0].Value<decimal>();
                var volume = obj[1].Value<decimal>();
                asks.Add(new BithumbWebSocketOrderbookSnapshotValue(price, volume));
            }

            var bidsData = (JsonConvert.DeserializeObject<IEnumerable<object>>(properties.GetString("asks")) ?? default!).Cast<JArray>();
            foreach (var obj in bidsData)
            {
                var price = obj[0].Value<decimal>();
                var volume = obj[1].Value<decimal>();
                bids.Add(new BithumbWebSocketOrderbookSnapshotValue(price, volume));
            }

            return new BithumbWebSocketOrderbookSnapshot(symbol, datetime, asks, bids);
        }

        public override void WriteJson(JsonWriter writer, BithumbWebSocketOrderbookSnapshot? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
