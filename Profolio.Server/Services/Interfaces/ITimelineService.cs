using Profolio.Server.Dto.Profile;

namespace Profolio.Server.Services.Interfaces
{
	public interface ITimelineService
	{
		Task<List<ProfileDto.Timeline>> GetTimeline();
		Task<bool> UpdateTimeline(ProfileDto.Timeline timeline);
	}
}