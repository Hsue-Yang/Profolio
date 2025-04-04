using Profolio.Server.Dto.Article;

namespace Profolio.Server.Services.Interfaces
{
	public interface IHackMdNoteTagsService
	{
		Task<bool> AddOrUpdateAsync(int tagID, HackMDNoteTagDto noteTag);
	}
}