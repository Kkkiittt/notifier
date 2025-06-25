using System.ComponentModel.DataAnnotations;

namespace ProfileManager.Application.Dtos.ProfileDtos;

public class EmailConfirmDto
{
	[EmailAddress]
	public string Email { get; set; }
	[Required]
	public string Code { get; set; }

	public EmailConfirmDto(string email, string code)
	{
		Email = email;
		Code = code;
	}
}
