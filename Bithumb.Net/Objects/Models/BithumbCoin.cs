namespace Bithumb.Net.Objects.Models
{
    public record BithumbCoin(string currency, decimal opening_price, decimal closing_price, decimal min_price, decimal max_price, decimal units_traded, decimal acc_trade_value, decimal prev_closing_price, decimal units_traded_24H, decimal acc_trade_value_24H, decimal fluctate_24H, decimal fluctate_rate_24H, long date);
}
