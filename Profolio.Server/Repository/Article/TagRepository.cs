using Profolio.Server.Dto.Article;
using Profolio.Server.Models;
using Profolio.Server.Models.Entities;
using Profolio.Server.Repository.Interfaces.Article;

namespace Profolio.Server.Repository.Article
{
    public class TagRepository : Repository<Tag>, ITagRepository
    {
        private readonly IConnectionStringProvider _connectionProvider;

        public TagRepository(ProfolioContext dbContext, IConnectionStringProvider connectionProvider) : base(dbContext)
        {
            _connectionProvider = connectionProvider;
        }

        public async Task<IEnumerable<TagArticleDto>> GetTagTreeArticle()
        {
            var tagData = from noteTag in _dbContext.HackMdnoteTags
                          join tag in _dbContext.Tags
                          on noteTag.TagID equals tag.ID
                          join note in _dbContext.HackMdnotes
                          on noteTag.NoteID equals note.NoteID
                          select new TagArticleDto
                          {
                              TagID = tag.ID,
                              Name = tag.Name,
                              ParentID = tag.ParentID,
                              NoteID = note.NoteID,
                              Title = note.Title,
                          };
            var nonTagData = from note in _dbContext.HackMdnotes
                             where string.IsNullOrWhiteSpace(note.Tags)
                             select new TagArticleDto
                             {
                                 TagID = 99,
                                 Name = "More",
                                 ParentID = null,
                                 NoteID = note.NoteID,
                                 Title = note.Title,
                             };

            return tagData.Union(nonTagData).OrderBy(d => d.TagID).ThenBy(d => d.ParentID); //.ThenByDescending(d => d.CreatedAt)
        }
    }
}