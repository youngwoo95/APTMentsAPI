namespace APTMentsAPI.Services.Helpers
{
    public interface IRequestAPI
    {
        void RequestMessage(HttpRequest request, string? dto = null);
    }
}
