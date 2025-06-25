using System.ComponentModel.DataAnnotations;

using ProfileManager.Domain.Enums;

namespace ProfileManager.Application.Dtos.NotificationDtos;

public class FilteredNotificationDto
{
	public Gender[]? AllowedGenders { get; set; }

	public int? MinimumAge { get; set; }
	public int? MaximumAge { get; set; }

	public Platform[]? NeededPlatforms { get; set; }

	public DateTime? MinimumLastOnline { get; set; }
	public DateTime? MaximumLastOnline { get; set; }

	public TagRequirementDto[]? TagRequirements { get; set; }

	[Length(4, 40)]
	public string Title { get; set; }
	[Length(4, 200)]
	public string Message { get; set; }

	public FilteredNotificationDto(string title, string message)
	{
		Title = title;
		Message = message;
	}
}

public class TagRequirementDto
{
	public long TagId { get; set; }

	public double? MinimumWeight { get; set; }
	public double? MaximumWeight { get; set; }

	public bool Required { get; set; }
}