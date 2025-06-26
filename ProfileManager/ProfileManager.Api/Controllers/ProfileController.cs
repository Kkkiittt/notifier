using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using ProfileManager.Application.Dtos.ProfileDtos;
using ProfileManager.Application.Interfaces.Services;

namespace ProfileManager.Api.Controllers;

[ApiController]
[Route("profiles")]
public class ProfileController : ControllerBase
{
	private readonly IProfileService _profServ;

	public ProfileController(IProfileService profServ)
	{
		_profServ = profServ;
	}

	[HttpPost("login")]
	[AllowAnonymous]
	public async Task<IActionResult> LoginAsync(ProfileLoginDto dto)
	{
		return Ok(await _profServ.LoginAsync(dto));
	}

	[HttpPost("register")]
	[AllowAnonymous]
	public async Task<IActionResult> RegisterAsync(ProfileCreateDto dto)
	{
		return Ok(await _profServ.CreateProfileAsync(dto));
	}

	[HttpGet("update")]
	[Authorize(Roles = "User")]
	public async Task<IActionResult> GetTemplateAsync()
	{
		return Ok(await _profServ.GetTemplateAsync());
	}

	[HttpPut("update")]
	[Authorize(Roles = "User")]
	public async Task<IActionResult> UpdateProfileAsync(ProfileUpdateDto dto)
	{
		return Ok(await _profServ.UpdateProfileAsync(dto));
	}

	[HttpDelete("{id}")]
	[Authorize]
	public async Task<IActionResult> DeleteProfileAsync(long id)
	{
		return Ok(await _profServ.DeleteProfileAsync(id));
	}

	[HttpGet("{id}")]
	[Authorize]
	public async Task<IActionResult> GetProfileAsync(long id)
	{
		return Ok(await _profServ.GetShortProfileAsync(id));
	}

	[HttpGet("{id}/full")]
	[Authorize(Roles = "SuperAdmin, Admin")]
	public async Task<IActionResult> GetFullProfileAsync(long id)
	{
		return Ok(await _profServ.GetFullProfileAsync(id));
	}

	[HttpGet]
	[Authorize(Roles = "SuperAdmin, Admin")]
	public async Task<IActionResult> GetAllProfilesAsync(int page, int pageSize)
	{
		return Ok(await _profServ.GetShortProfilesAsync(page, pageSize));
	}

	[HttpPut("password")]
	[AllowAnonymous]
	public async Task<IActionResult> ChangePasswordAsync(PasswordChangeDto dto)
	{
		return Ok(await _profServ.ChangePasswordAsync(dto));
	}

	[HttpPut("reset/{email}")]
	[AllowAnonymous]
	public async Task<IActionResult> ResetPasswordAsync(string email)
	{
		return Ok(await _profServ.ResetPasswordAsync(email));
	}

	[HttpPatch("confirm")]
	[AllowAnonymous]
	public async Task<IActionResult> ConfirmEmailAsync(EmailConfirmDto dto)
	{
		return Ok(await _profServ.ConfirmEmailAsync(dto));
	}

	[HttpGet("confirm/{email}")]
	[AllowAnonymous]
	public async Task<IActionResult> RequestConfirmEmailAsync(string email)
	{
		return Ok(await _profServ.RequestEmailConfirmationAsync(email));
	}
}
