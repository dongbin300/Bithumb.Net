using Bithumb.Net.Enums;

using Newtonsoft.Json.Linq;

namespace Bithumb.Net.Extensions
{
    public static class BithumbExtension
    {
        #region Timestamp
        public static long ToTimestampMs(this DateTime? dateTime)
        {
            if (dateTime == null)
            {
                return 0;
            }
            return (long)(dateTime.Value - new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds;
        }

        public static DateTime ToDateTime(this long timestamp)
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(timestamp).LocalDateTime;
        }
        #endregion

        #region Enum
        public static string EnumToString(this BithumbTransactionType? transactionType)
        {
            if (transactionType == null)
            {
                return string.Empty;
            }
            return transactionType.ToString() ?? default!;
        }

        public static string EnumToString(this BithumbInterval interval)
        {
            return interval switch
            {
                BithumbInterval.OneMinute => "1m",
                BithumbInterval.ThreeMinutes => "3m",
                BithumbInterval.FiveMinutes => "5m",
                BithumbInterval.TenMinutes => "10m",
                BithumbInterval.ThirtyMinutes => "30m",
                BithumbInterval.OneHour => "1h",
                BithumbInterval.SixHours => "6h",
                BithumbInterval.TwelveHours => "12h",
                BithumbInterval.OneDay => "24h",
                _ => "24h",
            };
        }

        public static string EnumToString(this BithumbSocketTickInterval interval)
        {
            return interval switch
            {
                BithumbSocketTickInterval.ThirtyMinutes => "30M",
                BithumbSocketTickInterval.OneHour => "1H",
                BithumbSocketTickInterval.TwelveHours => "12H",
                BithumbSocketTickInterval.OneDay => "24H",
                BithumbSocketTickInterval.Mid => "MID",
                _ => "",
            };
        }
        #endregion

        #region Dictionary
        public static void InsertAtBeginning(this IDictionary<string, string> dictionary, string key, string value)
        {
            var newDictionary = new Dictionary<string, string> { { key, value } };

            foreach (var pair in dictionary)
            {
                if (!newDictionary.ContainsKey(pair.Key))
                {
                    newDictionary.Add(pair.Key, pair.Value);
                }
            }

            dictionary.Clear();
            foreach (var pair in newDictionary)
            {
                dictionary.Add(pair.Key, pair.Value);
            }
        }
        #endregion

        #region Json
        public static string GetString(this IEnumerable<JProperty> properties, string key)
        {
            var match = properties.Where(p => p.Name.Equals(key));
            if(match == null || !match.Any())
            {
                return string.Empty;
            }

            return match.First().Value.ToString();
        }

        public static decimal GetDecimal(this IEnumerable<JProperty> properties, string key)
        {
            var match = properties.Where(p => p.Name.Equals(key));
            if (match == null || !match.Any())
            {
                return default!;
            }

            return match.First().Value.Value<decimal>();
        }
        #endregion
    }
}
