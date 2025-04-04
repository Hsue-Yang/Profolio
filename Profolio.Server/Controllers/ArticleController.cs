using Microsoft.AspNetCore.Mvc;
using Profolio.Server.Dto.Article;
using Profolio.Server.Services.Interfaces;

namespace Profolio.Server.Controllers
{
	public class ArticleController : BaseController
	{
		private readonly IArticleService _service;

		public ArticleController(IArticleService service)
		{
			_service = service;
		}

		/// <summary> 手動觸發同步文章列表，用於排程或後台操作 </summary>
		/// <returns></returns>
		[HttpPost("Sync")]
		public async Task<IActionResult> SyncArticles()
		{
			await _service.SyncArticles();
			return Ok();
		}

		/// <summary> 預設回傳noteID，在呼叫HackMD API取得文章內容 </summary>
		/// <param name="noteId"></param>
		/// <returns></returns>
		//[ResponseCache(Duration = 300, Location = ResponseCacheLocation.Any, NoStore = false)]
		[HttpGet("{noteID}")]
		public async Task<IActionResult> GetArticleDetail([FromRoute] string noteID)
		{
			var result = await _service.GetArticleDetail(noteID);
			return Ok(result);
		}

		/// <summary> 主頁顯示最多觀看次數的10篇文章 </summary>
		/// <returns></returns>
		[HttpGet("Cards")]
		public async Task<IActionResult> GetArticleCards()
		{
			var result = await _service.GetArticleCards();
			return Ok(result);
		}

		[HttpGet("SubTitle")]
		public async Task<IActionResult> GetSubTitle()
		{
			var result = await _service.GetSubTitle();
			return Ok(result);
		}

		[HttpGet("Blogs")]
		public async Task<IActionResult> GetBlogs([FromQuery] int page = 1, [FromQuery] int pageSize = 3)
		{
			var count = await _service.GetBlogPostCount();
			var result = await _service.GetBlogPost(page, pageSize);
			return Ok(new
			{
				data = result,
				totalCount = count
			});
		}

		[HttpGet("Search")]
		public async Task<IActionResult> GetSearchResults([FromQuery] string query)
		{
			if (string.IsNullOrWhiteSpace(query))
			{
				return Ok(new SearchArticleDto());
			}
			var data = await _service.GetSearchResults(query);

			return Ok(data);
		}
	}
}