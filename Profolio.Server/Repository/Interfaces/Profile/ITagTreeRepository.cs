using Profolio.Server.Models.Entities;

namespace Profolio.Server.Repository.Interfaces.Profile
{
	public interface ITagTreeRepository : IRepository<TagTree>
	{
		Task<List<TagTree>> GetTreeNodes();
		Task<List<Tag>> GetAllTags();
	}
}