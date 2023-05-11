using Bithumb.Net.Extensions;
using Bithumb.Net.Objects.Models;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Bithumb.Net.Converters
{
    public class BithumbWebSocketTickerConverter : JsonConverter<BithumbWebSocketTicker>
    {
        public override BithumbWebSocketTicker? ReadJson(JsonReader reader, Type objectType, BithumbWebSocketTicker? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);
            var properties = jsonObject.Properties();

            var date = properties.GetString("date");
            var time = properties.GetString("time");
            var dateTimeString = $"{date[0..4]}-{date[4..6]}-{date[6..8]} {time[0..2]}:{time[2..4]}:{time[4..6]}";
            var dateTime = DateTime.Parse(dateTimeString);

            return new BithumbWebSocketTicker(
                properties.GetDecimal("volumePower"),
                properties.GetDecimal("chgAmt"),
                properties.GetDecimal("chgRate"),
                properties.GetDecimal("prevClosePrice"),
                properties.GetDecimal("buyVolume"),
                properties.GetDecimal("sellVolume"),
                properties.GetDecimal("volume"),
                properties.GetDecimal("value"),
                properties.GetDecimal("highPrice"),
                properties.GetDecimal("lowPrice"),
                properties.GetDecimal("closePrice"),
                properties.GetDecimal("openPrice"),
                dateTime,
                properties.GetString("tickType"),
                properties.GetString("symbol")
                );
        }

        public override void WriteJson(JsonWriter writer, BithumbWebSocketTicker? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
