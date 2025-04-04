using Profolio.Server.Dto.Profile;
using Profolio.Server.Services.Interfaces;

namespace Profolio.Server.Services.Profile
{
	public class UserIntroCardService : IUserIntroCardService
	{
		public Task<List<UserIntroCardDto>> GetIntroCards()
		{
			throw new NotImplementedException();
		}

		public Task<bool> UpdateIntroCards(UserIntroCardDto cardDto)
		{
			throw new NotImplementedException();
		}
	}
}