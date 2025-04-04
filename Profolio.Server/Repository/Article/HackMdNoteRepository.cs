using Microsoft.EntityFrameworkCore;
using Profolio.Server.Models;
using Profolio.Server.Models.Entities;
using Profolio.Server.Repository.Interfaces.Article;

namespace Profolio.Server.Repository.Article
{
	public class HackMdNoteRepository : Repository<HackMDNote>, IHackMdNoteRepository
	{
		public HackMdNoteRepository(ProfolioContext dbContext) : base(dbContext) { }

		public async Task<List<HackMDNote>> GetTopArticlesByViews(int count)
		{
			return await _dbSet.OrderByDescending(h => h.Views).Take(count).ToListAsync() ?? [];
		}

		public async Task IncrementViewCount(string title)
		{
			var article = await FirstOrDefaultAsync(h => h.Title == title);
			if (article != null)
			{
				article.Views += 1;
				await UpdateAsync(article);
			}
		}

		public async Task<List<HackMDNote>> GetBlogPost(int page, int pageSize)
		{
			return await _dbSet.OrderByDescending(h => h.CreatedAt).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
		}

		public async Task<int> GetBlogPostCount() => await _dbSet.CountAsync();
	}
}