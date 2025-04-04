using Profolio.Server.Dto.Article;

namespace Profolio.Server.Services.Interfaces
{
	public interface IHackMdNoteService
	{
		Task<List<HackMDNoteDto>> GetTopArticlesByViews();
		Task<HackMDNoteDto> GetArticle(string noteID);
		Task AddAsync(HackMDNoteDto note);
		Task IncrementViewCount(string noteID);
		Task<bool> AddOrUpdateAsync(string noteID, HackMDNoteDto note);
		Task<List<HackMDNoteDto>> GetBlogPost(int page, int pageSize);
		Task<int> GetBlogPostCount();
		Task DeleteArticle(string noteID);
	}
}