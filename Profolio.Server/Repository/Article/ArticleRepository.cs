using MapsterMapper;
using Profolio.Server.Dto.Article;
using Profolio.Server.Models;
using Profolio.Server.Models.Entities;
using Profolio.Server.Repository.Interfaces.Article;

namespace Profolio.Server.Repository.Article
{
    public class ArticleRepository : Repository<HackMDNote>, IArticleRepository
    {
        private readonly IConnectionStringProvider _connectionProvider;
        private readonly IMapper _mapper;

        public ArticleRepository(ProfolioContext dbContext, IConnectionStringProvider connectionProvider, IMapper mapper) : base(dbContext)
        {
            _connectionProvider = connectionProvider;
            _mapper = mapper;
        }

        public async Task<IEnumerable<HackMDNoteDto>> GetSearchResult(string query)
        {
            var data = await ListAsync(q => q.Title.Contains(query));
            return _mapper.Map<IEnumerable<HackMDNoteDto>>(data);
        }
    }
}