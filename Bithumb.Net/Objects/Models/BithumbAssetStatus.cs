namespace Bithumb.Net.Objects.Models
{
    public record BithumbAssetStatus(string currency, bool withdrawal_status, bool deposit_status);

    public record BithumbAssetStatuses(IEnumerable<BithumbAssetStatus> assets);
}
