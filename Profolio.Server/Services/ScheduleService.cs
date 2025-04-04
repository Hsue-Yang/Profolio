using Profolio.Server.Services.Interfaces;

namespace Profolio.Server.Services
{
	public class ScheduleService : IScheduleService
	{
		private readonly IArticleService _hackMdnoteService;

		public ScheduleService(IArticleService hackMdnoteService)
		{
			_hackMdnoteService = hackMdnoteService;
		}

		public async Task SyncHackMdData()
		{
			await _hackMdnoteService.SyncArticles();
		}
	}
}