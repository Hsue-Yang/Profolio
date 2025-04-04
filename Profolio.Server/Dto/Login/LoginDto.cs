using System.ComponentModel.DataAnnotations;

namespace Profolio.Server.ViewModels.Login;

public class LoginDto
{
	[Required]
	public string Account { get; set; }
	[Required]
	public string Password { get; set; }
}