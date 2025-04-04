using Microsoft.AspNetCore.Mvc;
using Profolio.Server.Services.Interfaces;

namespace Profolio.Server.Controllers
{
    public class TagController : BaseController
    {
        private readonly ITagService _service;
        public TagController(ITagService service)
        {
            _service = service;
        }

        [HttpGet("TagTree")]
        public async Task<IActionResult> GetTagsWithArticles()
        {
            var result = await _service.GetTagTreeArticles();
            return Ok(result);
        }
    }
}