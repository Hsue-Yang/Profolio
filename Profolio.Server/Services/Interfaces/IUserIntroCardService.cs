using Profolio.Server.Dto.Profile;

namespace Profolio.Server.Services.Interfaces
{
	public interface IUserIntroCardService
	{
		Task<List<UserIntroCardDto>> GetIntroCards();
		Task<bool> UpdateIntroCards(UserIntroCardDto cardDto);
	}
}