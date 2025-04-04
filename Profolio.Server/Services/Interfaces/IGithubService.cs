namespace Profolio.Server.Services.Interfaces
{
	public interface IGithubService
	{
		Task<string> GetContent(string title);
		Task UpdateFile(string title, string content);
	}
}