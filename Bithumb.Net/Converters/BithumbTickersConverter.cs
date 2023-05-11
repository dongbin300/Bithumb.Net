using Bithumb.Net.Objects.Models;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Bithumb.Net.Converters
{
    public class BithumbTickersConverter : JsonConverter<BithumbTickers>
    {
        public override BithumbTickers? ReadJson(JsonReader reader, Type objectType, BithumbTickers? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);
            var properties = jsonObject.Properties();

            long date = 0;
            var coins = new List<BithumbCoin>();
            foreach (var coin in properties)
            {
                switch (coin.Name)
                {
                    case "date":
                        date = long.Parse(coin.Value.ToString());
                        break;

                    default:
                        var _coin = JsonConvert.DeserializeObject<BithumbCoin>(coin.Value.ToString()) ?? default!;
                        var __coin = new BithumbCoin(coin.Name, _coin.opening_price, _coin.closing_price, _coin.min_price, _coin.max_price, _coin.units_traded, _coin.acc_trade_value, _coin.prev_closing_price, _coin.units_traded_24H, _coin.acc_trade_value_24H, _coin.fluctate_24H, _coin.fluctate_rate_24H, _coin.date);
                        coins.Add(__coin);
                        break;
                }
            }

            return new BithumbTickers(date, coins);
        }

        public override void WriteJson(JsonWriter writer, BithumbTickers? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
