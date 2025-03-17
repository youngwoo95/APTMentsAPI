
using APTMentsAPI.Services.Logger;
using System.Text.Json;

namespace APTMentsAPI.Services.Helpers
{
    public class RequestAPI : IRequestAPI
    {
        private readonly ILoggerService LoggerService;

        public RequestAPI(ILoggerService _loggerservice)
        {
            this.LoggerService = _loggerservice;
        }

        public void RequestMessage(HttpRequest request, string? dto = null)
        {
            try
            {
                var apiUrl = $"{request.Scheme}://{request.Host}{request.Path}{request.QueryString}";
                if (String.IsNullOrWhiteSpace(dto))
                {
                    // 전달할 내용이 없을때 --> 그냥 HTTP 요청만 찍어냄.
                    LoggerService.FileAPIMessage($"[INFO] {apiUrl}");
                }
                else
                {
                    // 전달할 내용이 있을때 --> HTTP 요청과 함께 DTO도 함께 찍어냄
                    var serializedDto = JsonSerializer.Serialize(dto);
                    LoggerService.FileAPIMessage($"[INFO] {apiUrl} >> {serializedDto}");
                }
            }
            catch(Exception ex)
            {
                LoggerService.FileLogMessage(ex.ToString());
            }
        }
    }
}
