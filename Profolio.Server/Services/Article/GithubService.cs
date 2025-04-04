using Profolio.Server.Repository.Interfaces.Article;
using Profolio.Server.Services.Interfaces;
using System.Text;

namespace Profolio.Server.Services.Article
{
	public class GithubService : IGithubService
	{
		private readonly IGithubRepository _repo;
		private readonly string _githubDirectory = "Articles";

		public GithubService(IGithubRepository repo)
		{
			_repo = repo;
		}
		public async Task<string> GetContent(string title)
		{
			string fileName = $"{title}.md";
			var file = await _repo.GetFile(_githubDirectory, fileName);

			return file != null && !string.IsNullOrWhiteSpace(file.Content) ? Encoding.UTF8.GetString(Convert.FromBase64String(file.Content)) : string.Empty;
		}

		public async Task UpdateFile(string title, string content)
		{
			string fileName = $"{title}.md";
			await _repo.UpdateFile(_githubDirectory, fileName, content);
		}
	}
}