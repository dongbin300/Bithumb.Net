namespace Bithumb.Net.Objects.Models
{
    public record BithumbCandlestick(DateTime dateTime, decimal open, decimal high, decimal low, decimal close, decimal volume);
}
