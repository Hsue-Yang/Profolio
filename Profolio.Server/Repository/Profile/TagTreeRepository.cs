using Profolio.Server.Models;
using Profolio.Server.Models.Entities;
using Profolio.Server.Repository.Interfaces.Article;
using Profolio.Server.Repository.Interfaces.Profile;

namespace Profolio.Server.Repository.Profile
{
	public class TagTreeRepository : Repository<TagTree>, ITagTreeRepository
    {
        private readonly ITagRepository _tagRepo;
        public TagTreeRepository(ProfolioContext dbContext, ITagRepository tagRepo) : base(dbContext)
        {
            _tagRepo = tagRepo;
        }

        public async Task<List<Tag>> GetAllTags()
        {
            return await _tagRepo.ListAsync();
        }

        public async Task<List<TagTree>> GetTreeNodes()
        {
            return await ListAsync();
        }
    }
}