using ProfileManager.Application.Dtos.ProfileTagDtos;
using ProfileManager.Application.Dtos.TagDtos;
using ProfileManager.Domain.Entities;

namespace ProfileManager.Application.Dtos.ProfileDtos;

public class ProfileFullGetDto : ProfileShortGetDto
{
	public List<ProfileTagGetDto> WeightedTags { get; set; }

	public static explicit operator ProfileFullGetDto(Profile profile)
	{
		return new ProfileFullGetDto()
		{
			Id = profile.Id,
			Name = profile.Name,
			Email = profile.Email,
			Gender = profile.Gender,
			BirthDate = profile.BirthDate,
			LastOnline = profile.LastOnline,
			Created = profile.CreatedAt,
			Updated = profile.UpdatedAt,
			WeightedTags = profile.ProfileTags.Select(x => (ProfileTagGetDto)x).ToList()
		};
	}

	public ProfileFullGetDto() { WeightedTags = []; }
}

