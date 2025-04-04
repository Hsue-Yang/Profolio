using Profolio.Server.Dto;
using Profolio.Server.Repository.Interfaces.Article;
using Profolio.Server.Services.Interfaces;
using System.Text;

namespace Profolio.Server.Repository.Article
{
    public class GithubRepository : IGithubRepository
    {
        private readonly string _appName;
        private readonly string _githubRepo;
        private readonly string _githubToken;
        private readonly string _githubBranch;
        private readonly string _githubUsername;
        private readonly string _githubUrl;
        private readonly IHttpClientService _clientService;
        private readonly IConfiguration _configuration;

        public GithubRepository(IConfiguration configuration, IHttpClientService clientService)
        {
            _configuration = configuration;
            _clientService = clientService;
            _githubToken = "Github:Token";
            _appName = _configuration.GetValue("AppName", string.Empty);
            _githubRepo = _configuration.GetValue("Github:Repo", string.Empty);
            _githubBranch = _configuration.GetValue("Github:Branch", string.Empty);
            _githubUsername = _configuration.GetValue("Github:Username", string.Empty);
            _githubUrl = $"https://api.github.com/repos/{_githubUsername}/{_githubRepo}/contents";
        }


        public async Task<GithubResponseDto> GetFile(string dirPath, string fileName)
        {
            string url = $"{_githubUrl}/{dirPath}/{fileName}?ref={_githubBranch}";
            return await _clientService.GetAsync<GithubResponseDto>(url, _githubToken, _appName);
        }

        public async Task<List<GithubResponseDto>> GetFileList(string dirPath)
        {
            string url = $"{_githubUrl}/{dirPath}?ref={_githubBranch}";
            return await _clientService.GetAsync<List<GithubResponseDto>>(url, _githubToken, _appName);
        }

        public async Task UpdateFile(string dirPath, string fileName, string content)
        {
            var file = await GetFile(dirPath, fileName);
            var sha = file != null ? file.Sha : string.Empty;
            var base64Content = Convert.ToBase64String(Encoding.UTF8.GetBytes(content));
            var commitMsg = string.IsNullOrWhiteSpace(sha) ? "Create new file" : "Update file";
            string url = $"{_githubUrl}/{dirPath}/{fileName}?ref={_githubBranch}";
            var payload = new
            {
                message = commitMsg,
                content = base64Content,
                sha = sha,
                branch = _githubBranch
            };

            await _clientService.PutAsync(url, payload, _githubToken, _appName);
        }
    }
}