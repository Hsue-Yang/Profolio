using Profolio.Server.Dto.Profile;
using Profolio.Server.Services.Interfaces;

namespace Profolio.Server.Services.Profile
{
	public class ProfileService : IProfileService
    {
        private readonly ITagTreeService _tagTreeService;
        private readonly ITimelineService _timelineService;
        private readonly IUserIntroCardService _introCardService;
        public ProfileService(ITagTreeService tagTreeService, ITimelineService timelineService, IUserIntroCardService introCardService)
        {
            _tagTreeService = tagTreeService;
            _timelineService = timelineService;
            _introCardService = introCardService;
        }

        public async Task<List<UserIntroCardDto>> GetIntroCards()
        {
            return await _introCardService.GetIntroCards();
        }

        public async Task<ProfileDto> GetProfileData()
        {
            var timelines = await _timelineService.GetTimeline();
            var tagTreeNodes = await _tagTreeService.GetTreeNode();

            return new ProfileDto { Timelines = timelines, TagTreeNodes = tagTreeNodes };
        }

        public async Task<bool> UpdateIntroCards(UserIntroCardDto cardDto)
        {
            return await _introCardService.UpdateIntroCards(cardDto);
        }

        public async Task<bool> UpdateTimeline(ProfileDto.Timeline timeline)
        {
            return await _timelineService.UpdateTimeline(timeline);
        }
    }
}