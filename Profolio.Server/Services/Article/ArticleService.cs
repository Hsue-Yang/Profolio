using Ganss.Xss;
using Profolio.Server.Dto.Article;
using Profolio.Server.Repository.Interfaces.Article;
using Profolio.Server.Services.Interfaces;
using Profolio.Server.Tools;
using System.Data;

namespace Profolio.Server.Services.Article
{
	public class ArticleService : IArticleService
	{
		private const string hackMDUrl = "https://api.hackmd.io/v1/notes/";

		private readonly IArticleRepository _repo;
		private readonly IGithubService _githubService;
		private readonly ITagService _tagService;
		private readonly IHackMdNoteService _hackMdService;
		private readonly IHttpClientService _clientService;
		private readonly IHackMdNoteTagsService _hackMdTagService;
		//private readonly IBlobStorageService _blobStorageService;
		public ArticleService(IHackMdNoteService hackMdService, IHttpClientService clientService, ITagService tagService, IHackMdNoteTagsService hackMdTagService, IArticleRepository repo, IGithubService githubService)
		{
			_repo = repo;
			_tagService = tagService;
			_hackMdService = hackMdService;
			_clientService = clientService;
			_hackMdTagService = hackMdTagService;
			_githubService = githubService;
		}

		public async Task<List<HackMDNoteDto>> GetArticleCards()
		{
			// 依照觀看次數最多的10篇文章排序
			var data = await _hackMdService.GetTopArticlesByViews();
			return data;
		}

		/// <summary>
		/// 取得文章內容
		/// 檢查DB是否有該文章，若無則呼叫HackMD API取得文章內容，並寫入檔案，Views觸擊率+1
		/// </summary>
		/// <param name="noteID"></param>
		/// <returns></returns>
		public async Task<HackMDNoteDto> GetArticleDetail(string noteID)
		{
			if (string.IsNullOrWhiteSpace(noteID)) return null;
			var note = EncryptTool.AESDecrypt(noteID);
			var article = await _hackMdService.GetArticle(note);
			if (article == null)
			{
				var hackMDArticle = await _clientService.GetAsync<HackMDNoteDto>(hackMDUrl + note, "HackMD");
				if (hackMDArticle == null || string.IsNullOrWhiteSpace(hackMDArticle.Content)) return null;

				article = hackMDArticle;
				await _githubService.UpdateFile(article.Title, article.Content); //考慮github要不要用noteID儲存
				await _hackMdService.AddOrUpdateAsync(noteID, article);
			}

			await _hackMdService.IncrementViewCount(noteID);
			article.Content = await _githubService.GetContent(article.Title);
			article.NoteID = noteID;

			return article;
		}

		public async Task<List<TagDto>> GetSubTitle()
		{
			return await _tagService.GetSubTitle();
		}

		/// <summary> 將HackMD所有文章寫入DB </summary>
		/// <returns></returns>
		public async Task<bool> SyncArticles()
		{
			var articleModels = await _clientService.GetAsync<List<HackMDNoteDto>>(hackMDUrl, "HackMD");
			if (articleModels != null && articleModels.Any())
			{
				foreach (var article in articleModels)
				{
					article.NoteID = EncryptTool.AESEncrypt(article.NoteID);
					// 根據 UpdateAt的時間去更新文章，如果沒有UpdateAt且文章不存在資料庫，則insert，反之略過不用fetch content
					if (string.IsNullOrWhiteSpace(article.UpdatedAtString) == false && article.UpdatedAt.HasValue)
					{
						var exArticle = await _hackMdService.GetArticle(article.NoteID);
						if (exArticle != null && exArticle.UpdatedAt >= article.UpdatedAt)
						{
							continue;
						}
					}
					await _hackMdService.DeleteArticle(article.NoteID);

					// Tags要去對應NoteID文章的HackMDNoteTagDto標籤有沒有更換，所以要先去找文章的Tags去跟Tag table比對TagID，然後去跟HackMDNoteTagDto比對noteID跟TagID有沒有一致。
					if (article.Tags != null && article.Tags.Length != 0)
					{
						int? parentId = null;
						foreach (var tagName in article.Tags)
						{
							var tag = await _tagService.GetTag(tagName) ?? await _tagService.AddTag(tagName, parentId);
							parentId = tag.ID;
							await _hackMdTagService.AddOrUpdateAsync(tag.ID, new HackMDNoteTagDto { NoteID = article.NoteID, TagID = tag.ID });
						}
					}

					var content = await _clientService.GetAsync<HackMDNoteDto>(hackMDUrl + EncryptTool.AESDecrypt(article.NoteID), "HackMD");
					if (content != null && string.IsNullOrWhiteSpace(content.Content) == false)
					{
						await _githubService.UpdateFile(article.Title, content.Content);
						article.Content = content.Content.Length > 200 ? content.Content[..200] : content.Content;
					}
					await _hackMdService.AddOrUpdateAsync(article.NoteID, article);
				}

				return true;
			}

			return false;
		}

		public async Task<List<HackMDNoteDto>> GetBlogPost(int page, int pageSize)
		{
			var data = await _hackMdService.GetBlogPost(page, pageSize);
			return data;
		}

		public async Task<int> GetBlogPostCount()
		{
			return await _hackMdService.GetBlogPostCount();
		}

		public async Task<SearchArticleDto> GetSearchResults(string query)
		{
			var sanitizer = new HtmlSanitizer();
			var data = await _repo.GetSearchResult(sanitizer.Sanitize(query));
			return new SearchArticleDto
			{
				SearchArticles = data.Select(article => new SearchArticles
				{
					Title = article.Title,
					NoteID = article.NoteID
				}).ToList()
			};
		}
	}
}