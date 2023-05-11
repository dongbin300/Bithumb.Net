using Bithumb.Net.Objects.Models;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Bithumb.Net.Converters
{
    public class BithumbAssetStatusesConverter : JsonConverter<BithumbAssetStatuses>
    {
        public override BithumbAssetStatuses? ReadJson(JsonReader reader, Type objectType, BithumbAssetStatuses? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);
            var properties = jsonObject.Properties();

            var assets = new List<BithumbAssetStatus>();
            foreach (var asset in properties)
            {
                var _assets = JsonConvert.DeserializeObject<BithumbAssetStatus>(asset.Value.ToString()) ?? default!;
                var __assets = new BithumbAssetStatus(asset.Name, _assets.withdrawal_status, _assets.deposit_status);
                assets.Add(__assets);
            }

            return new BithumbAssetStatuses(assets);
        }

        public override void WriteJson(JsonWriter writer, BithumbAssetStatuses? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
