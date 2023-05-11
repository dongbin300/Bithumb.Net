using Bithumb.Net.Extensions;
using Bithumb.Net.Objects.Models;
using Bithumb.Net.Objects.Models.ResponseModels;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Bithumb.Net.Converters
{
    public class BithumbCandlesticksConverter : JsonConverter<BithumbCandlestickResponse>
    {
        public override BithumbCandlestickResponse? ReadJson(JsonReader reader, Type objectType, BithumbCandlestickResponse? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);
            var properties = jsonObject.Properties();

            var status = properties.GetString("status");
            var data = properties.GetString("data");
            var message = properties.GetString("message");

            var dataObject = JsonConvert.DeserializeObject<IEnumerable<object>>(data) ?? default!;
            
            var candlesticks = new List<BithumbCandlestick>();
            foreach(JArray obj in dataObject)
            {
                var dateTime = obj[0].Value<long>().ToDateTime();
                var open = obj[1].Value<decimal>();
                var high = obj[3].Value<decimal>();
                var low = obj[4].Value<decimal>();
                var close = obj[2].Value<decimal>();
                var volume = obj[5].Value<decimal>();
                candlesticks.Add(new BithumbCandlestick(dateTime, open, high, low, close, volume));
            }

            return new BithumbCandlestickResponse(status, candlesticks, message);
        }

        public override void WriteJson(JsonWriter writer, BithumbCandlestickResponse? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
