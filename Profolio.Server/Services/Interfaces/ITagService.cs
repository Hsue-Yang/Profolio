using Profolio.Server.Dto.Article;

namespace Profolio.Server.Services.Interfaces
{
    public interface ITagService
    {
        Task<TagDto> GetTag(string tagName);
        Task<TagDto> AddTag(string tagName, int? parentID);
        Task<List<TagDto>> GetSubTitle();
        Task<List<TreeViewDto>> GetTagTreeArticles();
    }
}