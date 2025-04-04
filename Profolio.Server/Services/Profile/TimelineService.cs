using MapsterMapper;
using Profolio.Server.Dto.Profile;
using Profolio.Server.Models.Entities;
using Profolio.Server.Repository.Interfaces.Profile;
using Profolio.Server.Services.Interfaces;

namespace Profolio.Server.Services.Profile
{
	public class TimelineService : ITimelineService
	{
		private readonly ITimelineRepository _repo;
		private readonly IMapper _mapper;
		public TimelineService(ITimelineRepository repo, IMapper mapper)
		{
			_repo = repo;
			_mapper = mapper;
		}

		public async Task<List<ProfileDto.Timeline>> GetTimeline()
		{
			return (await _repo.GetTimeline()).Select(t => _mapper.Map<ProfileDto.Timeline>(t)).ToList();
		}

		public async Task<bool> UpdateTimeline(ProfileDto.Timeline timeline)
		{
			if (timeline == null) { return false; };

			var existingTimeline = await _repo.FirstOrDefaultAsync(t => t.Title == timeline.Title);
			if (existingTimeline == null)
			{
				await _repo.AddAsync(_mapper.Map<Timeline>(timeline));
			}
			else
			{
				await _repo.UpdateAsync(_mapper.Map<Timeline>(existingTimeline));
			}

			return true;
		}
	}
}