namespace Bithumb.Net.Objects.Models
{
    public record BithumbOrderbook(string order_currency, IEnumerable<BithumbQuotationBalance> bids, IEnumerable<BithumbQuotationBalance> asks);
    public record BithumbOrderbooks(long timestamp, string payment_currency, IEnumerable<BithumbOrderbook> orderbooks);
}
