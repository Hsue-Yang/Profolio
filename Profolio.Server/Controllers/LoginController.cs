using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profolio.Server.Dto;
using Profolio.Server.Enums;
using Profolio.Server.Services.Login;
using Profolio.Server.ViewModels.Login;

namespace Profolio.Server.Controllers.LoginController
{
    public class LoginController : BaseController
    {
        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var validResult = await Validation.IsValidLogin(model);
            return Ok(new BaseResponseDto
            {
                Data = "QQ",
                Status = SuccessEnum.Success
            });
        }
    }
}