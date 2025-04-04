using Profolio.Server.Models.Entities;

namespace Profolio.Server.Repository.Interfaces.Article
{
	public interface IHackMdNoteRepository : IRepository<HackMDNote>
    {
        Task<List<HackMDNote>> GetTopArticlesByViews(int count);
        Task IncrementViewCount(string title);
        Task<List<HackMDNote>> GetBlogPost(int page, int pageSize);
        Task<int> GetBlogPostCount();
    }
}