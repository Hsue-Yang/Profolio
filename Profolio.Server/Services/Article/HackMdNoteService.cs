using MapsterMapper;
using Profolio.Server.Dto.Article;
using Profolio.Server.Models.Entities;
using Profolio.Server.Repository.Interfaces.Article;
using Profolio.Server.Services.Interfaces;

namespace Profolio.Server.Services.Article
{
    public class HackMdNoteService : IHackMdNoteService
    {
        private readonly IHackMdNoteRepository _repo;
        private readonly IMapper _mapper;
        public HackMdNoteService(IHackMdNoteRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<HackMDNoteDto>> GetTopArticlesByViews()
        {
            var note = await _repo.GetTopArticlesByViews(10);
            return _mapper.Map<List<HackMDNoteDto>>(note);
        }

        public async Task<HackMDNoteDto> GetArticle(string noteID)
        {
            var note = await _repo.FirstOrDefaultAsync(n => n.NoteID == noteID);
            return _mapper.Map<HackMDNoteDto>(note);
        }

        public async Task AddAsync(HackMDNoteDto note)
        {
            await _repo.AddAsync(_mapper.Map<HackMDNote>(note));
        }

        public async Task IncrementViewCount(string title)
        {
            await _repo.IncrementViewCount(title);
        }

        public async Task<List<HackMDNoteDto>> GetBlogPost(int page, int pageSize)
        {
            return _mapper.Map<List<HackMDNoteDto>>(await _repo.GetBlogPost(page, pageSize));
        }

        public async Task<int> GetBlogPostCount()
        {
            return await _repo.GetBlogPostCount();
        }

        public async Task<bool> AddOrUpdateAsync(string noteID, HackMDNoteDto note)
        {
            if (string.IsNullOrWhiteSpace(noteID) || note == null) return false;

            var entity = await _repo.FirstOrDefaultAsync(h => h.NoteID == noteID);
            if (entity != null)
            {
                await _repo.DeleteAsync(entity);
                await _repo.AddAsync(MapArticleToNote(note));
            }
            else
            {
                await _repo.AddAsync(MapArticleToNote(note));
            }

            return true;
        }

        private HackMDNote MapArticleToNote(HackMDNoteDto article, HackMDNote? existingNote = null)
        {
            var note = existingNote ?? new HackMDNote();

            note.NoteID = article.NoteID;
            note.Title = article.Title;
            note.CreatedAt = article.CreatedAt;
            note.UpdatedAt = article.UpdatedAt;
            note.Tags = article.Tags != null ? string.Join(",", article.Tags) : string.Empty;
            note.Content = article.Content.Length > 200 ? article.Content[..200] : article.Content;

            return note;
        }

        public async Task DeleteArticle(string noteID)
        {
            //利用noteID跟title去找entity，判斷Content[..200]有沒有變更，有才更新，最後回傳bool
            var entity = await _repo.FirstOrDefaultAsync(r => r.NoteID == noteID);
            if (entity != null) await _repo.DeleteAsync(entity);
        }
    }
}