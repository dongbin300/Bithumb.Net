namespace Bithumb.Net.Objects.Models
{
    public record BithumbAccount(long created, string account_id, string order_currency, string payment_currency, decimal trade_fee, decimal balance);
}
