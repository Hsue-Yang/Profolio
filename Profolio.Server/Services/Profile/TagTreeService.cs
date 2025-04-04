using Profolio.Server.Dto.Profile;
using Profolio.Server.Models.Entities;
using Profolio.Server.Repository.Interfaces.Profile;
using Profolio.Server.Services.Interfaces;

namespace Profolio.Server.Services.Profile
{
	public class TagTreeService : ITagTreeService
    {
        private readonly ITagTreeRepository _repo;
        public TagTreeService(ITagTreeRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<ProfileDto.TagTree>> GetTreeNode()
        {
            var tags = await _repo.GetAllTags();
            var skillTreeNodes = await _repo.GetTreeNodes();

            var rootTags = tags.Where(t => t.ParentID == null).ToList();
            var result = rootTags.Select(t => BuildTree(t, tags)).ToList();

            return result;
        }

        private ProfileDto.TagTree BuildTree(Tag tag, List<Tag> allTags)
        {
            var children = allTags.Where(a => a.ParentID == tag.ID).ToList();
            return new ProfileDto.TagTree
            {
                Name = tag.Name,
                Url = tag.Name, //組成Blog URL
                Children = children.Select(c => BuildTree(c, allTags)).ToList()
            };
        }
    }
}