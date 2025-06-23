using System.ComponentModel.DataAnnotations;

namespace AdminManager.Application.Dtos;

public class AdminLoginDto
{
	[EmailAddress]
	public string Email { get; set; }
	[Length(6, 20)]
	public string Password { get; set; }

	public AdminLoginDto(string email, string password)
	{
		Email = email;
		Password = password;
	}
}
