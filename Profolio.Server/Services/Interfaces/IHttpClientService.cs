namespace Profolio.Server.Services.Interfaces
{
    public interface IHttpClientService
    {
        /// <summary> 發送 GET 請求並反序列化 JSON 到指定的對象 </summary>
        Task<T?> GetAsync<T>(string url, string token = null, string userAgent = "") where T : class;

        /// <summary> 發送 POST 請求，提交 JSON 並反序列化 JSON 到指定的對象 </summary>
        Task<TResponse?> PostAsync<TRequest, TResponse>(string url, TRequest data, Dictionary<string, string> headers = null)
            where TRequest : class
            where TResponse : class;
        /// <summary> 發送 PUT 請求，提交 JSON 並反序列化 JSON 到指定的對象 </summary>
        Task PutAsync(string url, object data, string token = "", string userAgent = "");
    }
}