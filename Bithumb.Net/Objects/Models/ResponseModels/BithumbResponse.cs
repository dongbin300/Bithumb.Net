namespace Bithumb.Net.Objects.Models.ResponseModels
{
    public record BithumbResponse<T>(string status, T? data, string message);
}
