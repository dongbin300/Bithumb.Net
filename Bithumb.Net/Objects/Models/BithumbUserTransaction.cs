namespace Bithumb.Net.Objects.Models
{
    public record BithumbUserTransaction(string search, long transfer_date, string order_currency, string payment_currency, decimal units, decimal price, decimal amount, string fee_currency, decimal fee, decimal order_balance, decimal payment_balance);
}
