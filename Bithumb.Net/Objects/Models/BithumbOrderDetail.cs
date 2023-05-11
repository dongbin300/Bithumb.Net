using Bithumb.Net.Enums;

namespace Bithumb.Net.Objects.Models
{
    public record BithumbOrderDetail(long transaction_date, BithumbTransactionType type, string order_status, string order_currency, string payment_currency, decimal order_price, decimal order_qty, long? cancel_date, BithumbTransactionType? cancel_type, IEnumerable<BithumbContract> contract);
}
