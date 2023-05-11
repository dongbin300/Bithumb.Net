namespace Bithumb.Net.Objects.Models
{
    public record BithumbTrade(string order_currency, string payment_currency, decimal opening_price, decimal closing_price, decimal min_price, decimal max_price, decimal average_price, decimal units_traded, decimal volume_1day, decimal volume_7day, decimal fluctate_24H, decimal fluctate_rate_24H, long date);
}
