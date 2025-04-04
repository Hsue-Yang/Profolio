using Profolio.Server.Services.Interfaces;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Profolio.Server.Services
{
    public class HttpClientService : IHttpClientService
    {
        private readonly ILogger<HttpClientService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _clientFactory;

        public HttpClientService(IHttpClientFactory clientFactory, IConfiguration configuration, ILogger<HttpClientService> logger)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<T?> GetAsync<T>(string url, string token = "", string userAgent = "") where T : class
        {
            var client = _clientFactory.CreateClient();
            if (string.IsNullOrWhiteSpace(userAgent) == false)
            {
                client.DefaultRequestHeaders.UserAgent.ParseAdd(userAgent);

            }
            if (string.IsNullOrWhiteSpace(token) == false)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _configuration.GetValue(token, string.Empty));
            }
            using var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<T>(content);
        }

        public async Task<TResponse?> PostAsync<TRequest, TResponse>(string url, TRequest data, Dictionary<string, string> headers = null)
            where TRequest : class
            where TResponse : class
        {
            var client = _clientFactory.CreateClient();
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    client.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);
                }
            }
            var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
            using var response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<TResponse>(responseContent);
        }

        public async Task PutAsync(string url, object data, string token = "", string userAgent = "")
        {
            try
            {
                var client = _clientFactory.CreateClient();
                if (string.IsNullOrWhiteSpace(userAgent) == false)
                {
                    client.DefaultRequestHeaders.UserAgent.ParseAdd(userAgent);
                }
                if (string.IsNullOrWhiteSpace(token) == false)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _configuration.GetValue<string>(token));
                }
                var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
                using var response = await client.PutAsync(url, content);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return;
            }
        }
    }
}