using MapsterMapper;
using Profolio.Server.Dto.Article;
using Profolio.Server.Models.Entities;
using Profolio.Server.Repository.Interfaces.Article;
using Profolio.Server.Services.Interfaces;

namespace Profolio.Server.Services.Article
{
	public class HackMdNoteTagsService : IHackMdNoteTagsService
	{
		private readonly IHackMdNoteTagsRepository _repo;
		private readonly IMapper _mapper;
		public HackMdNoteTagsService(IHackMdNoteTagsRepository repo, IMapper mapper)
		{
			_repo = repo;
			_mapper = mapper;
		}

		public async Task<bool> AddOrUpdateAsync(int tagID, HackMDNoteTagDto noteTag)
		{
			if (tagID == 0 || noteTag == null) return false;

			var entity = await _repo.FirstOrDefaultAsync(h => h.NoteID == noteTag.NoteID && h.TagID == tagID);
			if (entity != null)
			{
				await _repo.DeleteAsync(entity);
				await _repo.AddAsync(_mapper.Map<HackMDNoteTag>(noteTag));
			}
			else
			{
				await _repo.AddAsync(_mapper.Map<HackMDNoteTag>(noteTag));
			}

			return true;
		}
	}
}