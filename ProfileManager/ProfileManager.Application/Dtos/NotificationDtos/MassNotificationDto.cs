using System.ComponentModel.DataAnnotations;

namespace ProfileManager.Application.Dtos.NotificationDtos;

public class MassNotificationDto
{
	[Required]
	public long[] ProfileIds { get; set; }

	[Length(4, 20)]
	public string Title { get; set; }
	[Length(4, 200)]
	public string Message { get; set; }

	public MassNotificationDto(long[] profileIds, string title, string message)
	{
		ProfileIds = profileIds;
		Title = title;
		Message = message;
	}
}
