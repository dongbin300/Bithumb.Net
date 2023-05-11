namespace Bithumb.Net.Objects.Models
{
    public record BithumbTickers(long date, IEnumerable<BithumbCoin> coins); 
}
