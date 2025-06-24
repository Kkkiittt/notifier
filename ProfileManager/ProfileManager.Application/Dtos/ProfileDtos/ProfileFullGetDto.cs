using ProfileManager.Application.Dtos.TagDtos;
using ProfileManager.Domain.Enums;

namespace ProfileManager.Application.Dtos.ProfileDtos;

public class ProfileFullGetDto : ProfileShortGetDto
{
	public List<(TagGetDto tag, double weight)> WeightedTags { get; set; }

	public ProfileFullGetDto(long id, string name, string email, Gender gender, DateTime birthdate, DateTime lastOnline, DateTime created, DateTime? updated = null, List<(TagGetDto tag, double weight)>? tags = null) : base(id, name, email, gender, birthdate, lastOnline, created, updated)
	{
		WeightedTags = tags ?? new List<(TagGetDto tag, double weight)>();
	}

	public ProfileFullGetDto() { WeightedTags = new List<(TagGetDto tag, double weight)>(); }
}
