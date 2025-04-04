namespace Profolio.Server.Services.Interfaces
{
    public interface IErrorLogService
    {
        Task LogErrorAsync(string error, string errorInfo);
    }
}