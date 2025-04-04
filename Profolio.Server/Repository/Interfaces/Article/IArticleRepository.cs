using Profolio.Server.Dto.Article;
namespace Profolio.Server.Repository.Interfaces.Article
{
    public interface IArticleRepository
    {
        Task<IEnumerable<HackMDNoteDto>> GetSearchResult(string query);
    }
}