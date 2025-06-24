using System.ComponentModel.DataAnnotations;

namespace ProfileManager.Application.Dtos.ProfileDtos;

public class ProfileLoginDto
{
	[EmailAddress]
	public string Email { get; set; }
	[Length(8, 30)]
	public string Password { get; set; }

	public ProfileLoginDto(string email, string password)
	{
		Email = email;
		Password = password;
	}
}
