using ProfileManager.Domain.Entities;
using ProfileManager.Domain.Enums;

namespace ProfileManager.Application.Dtos.ProfileDtos;

public class ProfileShortGetDto
{
	public long Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public Gender Gender { get; set; }
	public DateTime BirthDate { get; set; }
	public DateTime? LastOnline { get; set; }
	public DateTime Created { get; set; }
	public DateTime? Updated { get; set; }

	public static explicit operator ProfileShortGetDto(Profile profile)
	{
		return new ProfileShortGetDto()
		{
			Id = profile.Id,
			Name = profile.Name,
			Email = profile.Email,
			Gender = profile.Gender,
			BirthDate = profile.BirthDate,
			LastOnline = profile.LastOnline,
			Created = profile.CreatedAt,
			Updated = profile.UpdatedAt
		};
	}

	public ProfileShortGetDto() { }
}
