using Profolio.Server.Dto.Article;

namespace Profolio.Server.Services.Interfaces
{
    public interface IArticleService
    {
        Task<List<HackMDNoteDto>> GetArticleCards();
        Task<HackMDNoteDto> GetArticleDetail(string noteID);
        Task<bool> SyncArticles();
        Task<List<TagDto>> GetSubTitle();
        Task<List<HackMDNoteDto>> GetBlogPost(int page, int pageSize);
        Task<int> GetBlogPostCount();
        Task<SearchArticleDto> GetSearchResults(string query);
    }
}