using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using ProfileManager.Application.Dtos.NotificationDtos;
using ProfileManager.Application.Interfaces.Services;

namespace ProfileManager.Api.Controllers;

[ApiController]
[Route("notifications")]
public class NotificationController : ControllerBase
{
	private readonly INotificationService _notServ;

	public NotificationController(INotificationService notServ)
	{
		_notServ = notServ;
	}

	[HttpPost("massive")]
	[Authorize(Roles = "Admin, SuperAdmin")]
	public async Task<IActionResult> SendMassiveNotificationAsync(MassNotificationDto dto)
	{
		try
		{
			return Ok(await _notServ.SendMassNotificationAsync(dto));
		}
		catch(Exception ex) { return BadRequest(ex.Message); }
	}

	[HttpPost("filtered")]
	[Authorize(Roles = "Admin, SuperAdmin")]
	public async Task<IActionResult> SendFilteredNotificationAsync(FilteredNotificationDto dto)
	{
		try
		{
			return Ok(await _notServ.SendFilteredNotificationAsync(dto));
		}
		catch(Exception ex) { return BadRequest(ex.Message); }
	}
}
