using ProfileManager.Domain.Enums;

using System.ComponentModel.DataAnnotations;

namespace ProfileManager.Application.Dtos.ProfileDtos;

public class ProfileUpdateDto
{
	[Required]
	public long Id { get; set; }
	[Length(2, 30)]
	public string Name { get; set; }
	[EmailAddress]
	public string Email { get; set; }
	public string? Password { get; set; }
	[Required]
	public Gender Gender { get; set; }
	[Required]
	public DateTime Birthdate { get; set; }
	public List<Platform> Platforms { get; set; }

	public ProfileUpdateDto(long id, string name, string email, string? password, Gender gender, DateTime birthdate, List<Platform>? platforms = null)
	{
		Id = id;
		Name = name;
		Email = email;
		Password = password;
		Gender = gender;
		Birthdate = birthdate;
		Platforms = platforms ?? [];
	}
}
