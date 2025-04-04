using Profolio.Server.Dto.Profile;

namespace Profolio.Server.Services.Interfaces
{
	public interface IProfileService
	{
		Task<ProfileDto> GetProfileData();
		Task<bool> UpdateTimeline(ProfileDto.Timeline timeline);
		Task<List<UserIntroCardDto>> GetIntroCards();
		Task<bool> UpdateIntroCards(UserIntroCardDto cardDto);
	}
}