using System.ComponentModel.DataAnnotations;

namespace ProfileManager.Application.Dtos;

public class TagLinkDto
{
	[Required]
	public long TagId { get; set; }
	[Required]
	public long ProfileId { get; set; }

	[Required]
	public double Weight { get; set; }

	public TagLinkDto(long tagId, long profileId, double weight)
	{
		TagId = tagId;
		ProfileId = profileId;
		Weight = weight;
	}
}
