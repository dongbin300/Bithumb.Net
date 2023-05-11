namespace Bithumb.Net.Objects.Models
{
    public record BithumbWebSocketTransaction(string updn, DateTime contDtm, decimal contAmt, decimal contQty, decimal contPrice, string buySellGb, string symbol);

    public record BithumbWebSocketTransactions(IEnumerable<BithumbWebSocketTransaction> list);
}
