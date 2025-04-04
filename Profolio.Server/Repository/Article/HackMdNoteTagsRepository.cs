using Profolio.Server.Models;
using Profolio.Server.Models.Entities;
using Profolio.Server.Repository.Interfaces.Article;

namespace Profolio.Server.Repository.Article
{
	public class HackMdNoteTagsRepository : Repository<HackMDNoteTag>, IHackMdNoteTagsRepository
	{
		public HackMdNoteTagsRepository(ProfolioContext dbContext) : base(dbContext) { }
	}
}