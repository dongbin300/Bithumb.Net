using Bithumb.Net.Enums;

namespace Bithumb.Net.Objects.Models
{
    public record BithumbWebSocketOrderbookDepth(int total, BithumbTransactionType orderType, decimal quantity, decimal price, string symbol);

    public record BithumbWebSocketOrderbookDepths(DateTime datetime, IEnumerable<BithumbWebSocketOrderbookDepth> list);
}
