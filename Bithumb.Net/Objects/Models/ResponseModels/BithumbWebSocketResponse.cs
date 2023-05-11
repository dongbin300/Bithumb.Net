namespace Bithumb.Net.Objects.Models.ResponseModels
{
    public record BithumbWebSocketResponse<T>(string type, T content);
}
