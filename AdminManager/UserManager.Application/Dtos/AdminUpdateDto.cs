using System.ComponentModel.DataAnnotations;

namespace AdminManager.Application.Dtos;

public class AdminUpdateDto
{
	[Required]
	public long Id { get; set; }
	[Length(2, 20)]
	public string Name { get; set; }
	[EmailAddress]
	public string Email { get; set; }
	public string? Password { get; set; }

	public AdminUpdateDto(long id, string name, string email, string? password = null)
	{
		Id = id;
		Name = name;
		Email = email;
		Password = password;
	}
}
