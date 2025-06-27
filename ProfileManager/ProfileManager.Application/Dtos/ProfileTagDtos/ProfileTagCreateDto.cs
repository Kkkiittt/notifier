using System.ComponentModel.DataAnnotations;

namespace ProfileManager.Application.Dtos.ProfileTagDtos;

public class ProfileTagCreateDto
{
	[Required]
	public long TagId { get; set; }
	[Required]
	public long ProfileId { get; set; }

	[Required]
	public double Weight { get; set; }

	public ProfileTagCreateDto(long tagId, long profileId, double weight)
	{
		TagId = tagId;
		ProfileId = profileId;
		Weight = weight;
	}
}
