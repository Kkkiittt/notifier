using ProfileManager.Domain.Entities;

namespace ProfileManager.Application.Dtos.ProfileTagDtos;

public class ProfileTagGetDto
{
	public long TagId { get; set; }

	public string TagName { get; set; } = string.Empty;
	public double Weight { get; set; }

	public static explicit operator ProfileTagGetDto(ProfileTag profTag)
	{
		return new ProfileTagGetDto()
		{
			TagId = profTag.TagId,
			TagName = profTag.Tag.Name,
			Weight = profTag.Weight
		};
	}
}
