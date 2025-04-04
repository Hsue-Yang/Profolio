using Profolio.Server.Dto;
using Profolio.Server.Enums;
using Profolio.Server.ViewModels.Login;

namespace Profolio.Server.Services.Login
{
	public class Validation
    {
        public static async Task<BaseResponseDto> IsValidLogin(LoginDto model)
        {
            if (model.Account == "" && model.Password == "")
            {
                return new BaseResponseDto()
                {
                    Status = SuccessEnum.Success,
                };
            }

            return new BaseResponseDto
            {
                Status = SuccessEnum.Success,
            };
        }
    }
}
