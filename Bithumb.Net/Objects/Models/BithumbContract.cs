namespace Bithumb.Net.Objects.Models
{
    public record BithumbContract(long transaction_date, decimal price, decimal units, string fee_currency, decimal fee, decimal total);
}
