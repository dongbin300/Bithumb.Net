using Bithumb.Net.Enums;

namespace Bithumb.Net.Objects.Models
{
    public record BithumbOrder(string order_currency, string payment_currency, string order_id, long order_date, BithumbTransactionType type, decimal watch_price, decimal units, decimal units_remaining, decimal price);
}
