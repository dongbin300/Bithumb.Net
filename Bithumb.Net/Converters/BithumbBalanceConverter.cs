using Bithumb.Net.Objects.Models;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Bithumb.Net.Converters
{
    public class BithumbBalanceConverter : JsonConverter<BithumbBalance>
    {
        public override BithumbBalance? ReadJson(JsonReader reader, Type objectType, BithumbBalance? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);
            var properties = jsonObject.Properties();

            var totalKrw = jsonObject["total_krw"]?.ToObject<decimal>() ?? default!;
            var inuseKrw = jsonObject["in_use_krw"]?.ToObject<decimal>() ?? default!;
            var availableKrw = jsonObject["available_krw"]?.ToObject<decimal>() ?? default!;

            var currencies = properties.Where(p => p.Name.StartsWith("total_") && !p.Name.EndsWith("krw")).Select(p => p.Name.Replace("total_", ""));

            var coinBalances = new List<BithumbCoinBalance>();
            foreach (var currency in currencies)
            {
                var total = jsonObject["total_" + currency]?.ToObject<decimal>() ?? default!;
                var inuse = jsonObject["in_use_" + currency]?.ToObject<decimal>() ?? default!;
                var available = jsonObject["available_" + currency]?.ToObject<decimal>() ?? default!;
                var xcoinlast = jsonObject["xcoin_last_" + currency]?.ToObject<decimal>() ?? default!;
                coinBalances.Add(new BithumbCoinBalance(currency, total, inuse, available, xcoinlast));
            }

            return new BithumbBalance(totalKrw, inuseKrw, availableKrw, coinBalances);
        }

        public override void WriteJson(JsonWriter writer, BithumbBalance? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
