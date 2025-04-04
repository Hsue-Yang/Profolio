using Profolio.Server.Models.Entities;

namespace Profolio.Server.Repository.Interfaces.Profile
{
	public interface ITimelineRepository : IRepository<Timeline>
	{
		Task<List<Timeline>> GetTimeline();
	}
}