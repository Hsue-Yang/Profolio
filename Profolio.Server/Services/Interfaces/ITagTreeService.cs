using Profolio.Server.Dto.Profile;

namespace Profolio.Server.Services.Interfaces
{
	public interface ITagTreeService
	{
		Task<List<ProfileDto.TagTree>> GetTreeNode();
	}
}