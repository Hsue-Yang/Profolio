using Profolio.Server.Services.Interfaces;

namespace Profolio.Server.Services.ErrorLog
{
    public class ErrorLogService : IErrorLogService
    {
        private readonly ILogger<ErrorLogService> _logger;

        public ErrorLogService(ILogger<ErrorLogService> logger)
        {
            _logger = logger;
        }

        public async Task LogErrorAsync(string error, string errorInfo)
        {
            await Task.Run(() =>
            {
                _logger.LogError(error, errorInfo);
            });
        }
    }
}