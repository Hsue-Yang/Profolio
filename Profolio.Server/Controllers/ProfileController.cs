using Microsoft.AspNetCore.Mvc;
using Profolio.Server.Dto.Profile;
using Profolio.Server.Services.Interfaces;

namespace Profolio.Server.Controllers
{
    public class ProfileController : BaseController
    {
        private readonly IProfileService _service;

        public ProfileController(IProfileService service)
        {
            _service = service;
        }
        /// <summary> 技能樹跟時光背景可以放同一支API </summary>
        /// <returns></returns>
        [HttpGet("Overview")]
        public async Task<IActionResult> GetProfileData()
        {
            var result = await _service.GetProfileData();
            return Ok(result);
        }

        /// <summary> 後臺API，更新/新增 時光背景 </summary>
        /// <returns></returns>
        [HttpPost("Timeline")]
        public async Task<IActionResult> UpdateTimeline([FromBody] ProfileDto.Timeline timeline)
        {
            var result = await _service.UpdateTimeline(timeline);
            if (result == false)
            {
                return BadRequest();
            }

            return Ok("Timeline data saved successfully");
        }

        [HttpGet("IntroCards")]
        public async Task<IActionResult> GetIntroCards()
        {
            var cards = await _service.GetIntroCards();
            return Ok(cards);
        }

        [HttpPost("IntroCards")]
        public async Task<IActionResult> UpdateIntroCards([FromBody] UserIntroCardDto cardDto)
        {
            var result = await _service.UpdateIntroCards(cardDto);
            return Ok(result);
        }
    }
}