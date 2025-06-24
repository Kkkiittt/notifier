using ProfileManager.Domain.Enums;

namespace ProfileManager.Application.Dtos.ProfileDtos;

public class ProfileShortGetDto
{
	public long Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public Gender Gender { get; set; }
	public DateTime Birthdate { get; set; }
	public DateTime LastOnline { get; set; }
	public DateTime Created { get; set; }
	public DateTime? Updated { get; set; }

	public ProfileShortGetDto(long id, string name, string email, Gender gender, DateTime birthdate, DateTime lastOnline, DateTime created, DateTime? updated = null)
	{
		Id = id;
		Name = name;
		Email = email;
		Gender = gender;
		Birthdate = birthdate;
		LastOnline = lastOnline;
		Created = created;
		Updated = updated;
	}

	public ProfileShortGetDto() { }
}
