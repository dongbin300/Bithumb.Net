namespace Bithumb.Net.Objects.Models
{
    public record BithumbBalance(decimal total_krw, decimal in_use_krw, decimal available_krw, IEnumerable<BithumbCoinBalance> coin_balances);

    public record BithumbCoinBalance(string currency, decimal total, decimal in_use, decimal available, decimal xcoin_last);
}
