using ProfileManager.Application.Dtos.NotificationDtos;

namespace ProfileManager.Application.Interfaces.Services;

public interface INotificationService
{
	public Task<int> SendMassNotificationAsync(MassNotificationDto dto);

	public Task<int> SendFilteredNotificationAsync(FilteredNotificationDto dto);
}
