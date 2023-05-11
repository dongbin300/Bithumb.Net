using Bithumb.Net.Extensions;
using Bithumb.Net.Objects.Models;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System.Text;

namespace Bithumb.Net.Converters
{
    public class BithumbWebSocketOrderbookDepthsConverter : JsonConverter<BithumbWebSocketOrderbookDepths>
    {
        public override BithumbWebSocketOrderbookDepths? ReadJson(JsonReader reader, Type objectType, BithumbWebSocketOrderbookDepths? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);
            var properties = jsonObject.Properties();

            var datetime = (long.Parse(properties.GetString("datetime")) / 1000).ToDateTime();
            var depths = JsonConvert.DeserializeObject<IEnumerable<BithumbWebSocketOrderbookDepth>>(properties.GetString("list")) ?? default!;

            return new BithumbWebSocketOrderbookDepths(datetime, depths);
        }

        public override void WriteJson(JsonWriter writer, BithumbWebSocketOrderbookDepths? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
