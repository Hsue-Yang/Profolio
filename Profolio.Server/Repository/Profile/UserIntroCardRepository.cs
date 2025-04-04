using Profolio.Server.Models;
using Profolio.Server.Models.Entities;
using Profolio.Server.Repository.Interfaces.Profile;

namespace Profolio.Server.Repository.Profile
{
	public class UserIntroCardRepository : Repository<UserIntroCard>, IUserIntroCardRepository
    {
        public UserIntroCardRepository(ProfolioContext dbcontext) : base(dbcontext) { }
    }
}