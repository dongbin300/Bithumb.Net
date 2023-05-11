namespace Bithumb.Net.Objects.Models
{
    public record BithumbWebSocketTicker(decimal volumePower, decimal chgAmt, decimal chgRate, decimal prevClosePrice, decimal buyVolume, decimal sellVolume, decimal volume, decimal value, decimal highPrice, decimal lowPrice, decimal closePrice, decimal openPrice, DateTime dateTime, string tickType, string symbol);
}
