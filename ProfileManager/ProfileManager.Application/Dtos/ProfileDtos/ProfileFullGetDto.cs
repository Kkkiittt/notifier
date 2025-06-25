using ProfileManager.Application.Dtos.TagDtos;
using ProfileManager.Domain.Entities;
using ProfileManager.Domain.Enums;

namespace ProfileManager.Application.Dtos.ProfileDtos;

public class ProfileFullGetDto : ProfileShortGetDto
{
	public List<(TagGetDto tag, double weight)> WeightedTags { get; set; }

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
			WeightedTags = profile.ProfileTags.Select(x => ((TagGetDto)x.Tag, x.Weight)).ToList()
		};
	}

	public ProfileFullGetDto() { WeightedTags = new List<(TagGetDto tag, double weight)>(); }
}
