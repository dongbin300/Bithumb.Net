namespace Bithumb.Net.Objects.Models.ResponseModels
{
    public record BithumbCandlestickResponse(string status, IEnumerable<BithumbCandlestick> data, string message);
}
