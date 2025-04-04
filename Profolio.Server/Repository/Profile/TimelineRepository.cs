using Profolio.Server.Models;
using Profolio.Server.Models.Entities;
using Profolio.Server.Repository.Interfaces.Profile;

namespace Profolio.Server.Repository.Profile
{
	public class TimelineRepository : Repository<Timeline>, ITimelineRepository
	{
		public TimelineRepository(ProfolioContext dbContext) : base(dbContext) { }

		public async Task<List<Timeline>> GetTimeline()
		{
			var result = await OrderByDescendingAsync(t => t.TimePoint);
			return result ?? [];
		}
	}
}