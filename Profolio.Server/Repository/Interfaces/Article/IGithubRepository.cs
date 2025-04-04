using Profolio.Server.Dto;

namespace Profolio.Server.Repository.Interfaces.Article
{
	public interface IGithubRepository
	{
		Task<GithubResponseDto> GetFile(string dirPath, string fileName);
		Task<List<GithubResponseDto>> GetFileList(string dirPath);
		Task UpdateFile(string dirPath, string fileName, string content);
	}
}