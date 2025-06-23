using System.ComponentModel.DataAnnotations;

namespace AdminManager.Application.Dtos;

public class AdminCreateDto
{
	[Length(2, 20)]
	public string Name { get; set; }
	[EmailAddress]
	public string Email { get; set; }
	[Length(6, 20)]
	public string Password { get; set; }

	public AdminCreateDto(string name, string email, string password)
	{
		Name = name;
		Email = email;
		Password = password;
	}
}
