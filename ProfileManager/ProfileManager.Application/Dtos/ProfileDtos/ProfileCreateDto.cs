using System.ComponentModel.DataAnnotations;

using ProfileManager.Domain.Enums;

namespace ProfileManager.Application.Dtos.ProfileDtos;

public class ProfileCreateDto
{
	[Length(2, 30)]
	public string Name { get; set; }
	[EmailAddress]
	public string Email { get; set; }
	[Length(8, 30)]
	public string Password { get; set; }
	[Required]
	public Gender Gender { get; set; }
	[Required]
	public DateTime Birthdate { get; set; }
	public List<Platform> Platforms { get; set; }

	public ProfileCreateDto(string name, string email, string password, Gender gender, DateTime birthdate, List<Platform>? platforms = null)
	{
		Name = name;
		Email = email;
		Password = password;
		Gender = gender;
		Birthdate = birthdate;
		Platforms = platforms ?? [];
	}
}
