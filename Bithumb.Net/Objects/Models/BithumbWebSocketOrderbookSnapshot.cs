namespace Bithumb.Net.Objects.Models
{
    public record BithumbWebSocketOrderbookSnapshotValue(decimal price, decimal volume);

    public record BithumbWebSocketOrderbookSnapshot(string symbol, DateTime datetime, IEnumerable<BithumbWebSocketOrderbookSnapshotValue> asks, IEnumerable<BithumbWebSocketOrderbookSnapshotValue> bids);
}
