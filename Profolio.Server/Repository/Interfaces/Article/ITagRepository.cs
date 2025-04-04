using Profolio.Server.Dto.Article;
using Profolio.Server.Models.Entities;

namespace Profolio.Server.Repository.Interfaces.Article
{
    public interface ITagRepository : IRepository<Tag>
    {
        Task<IEnumerable<TagArticleDto>> GetTagTreeArticle();
    }
}