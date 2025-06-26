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
		try
		{
			return Ok(await _profServ.LoginAsync(dto));
		}
		catch(Exception ex) { return BadRequest(ex.Message); }
	}

	[HttpPost("register")]
	[AllowAnonymous]
	public async Task<IActionResult> RegisterAsync(ProfileCreateDto dto)
	{
		try
		{
			return Ok(await _profServ.CreateProfileAsync(dto));
		}
		catch(Exception ex) { return BadRequest(ex.Message); }
	}

	[HttpGet("update")]
	[Authorize(Roles = "User")]
	public async Task<IActionResult> GetTemplateAsync()
	{
		try
		{
			return Ok(await _profServ.GetTemplateAsync());
		}
		catch(Exception ex) { return BadRequest(ex.Message); }
	}

	[HttpPut("update")]
	[Authorize(Roles = "User")]
	public async Task<IActionResult> UpdateProfileAsync(ProfileUpdateDto dto)
	{
		try
		{
			return Ok(await _profServ.UpdateProfileAsync(dto));
		}
		catch(Exception ex) { return BadRequest(ex.Message); }
	}

	[HttpDelete("{id}")]
	[Authorize]
	public async Task<IActionResult> DeleteProfileAsync(long id)
	{
		try
		{
			return Ok(await _profServ.DeleteProfileAsync(id));
		}
		catch(Exception ex) { return BadRequest(ex.Message); }
	}

	[HttpGet("{id}")]
	[Authorize]
	public async Task<IActionResult> GetProfileAsync(long id)
	{
		try
		{
			return Ok(await _profServ.GetShortProfileAsync(id));
		}
		catch(Exception ex) { return BadRequest(ex.Message); }
	}

	[HttpGet("{id}/full")]
	[Authorize(Roles = "SuperAdmin, Admin")]
	public async Task<IActionResult> GetFullProfileAsync(long id)
	{
		try
		{
			return Ok(await _profServ.GetFullProfileAsync(id));
		}
		catch(Exception ex) { return BadRequest(ex.Message); }
	}

	[HttpGet]
	[Authorize(Roles = "SuperAdmin, Admin")]
	public async Task<IActionResult> GetAllProfilesAsync(int page=1, int pageSize=50)
	{
		try
		{
			return Ok(await _profServ.GetShortProfilesAsync(page, pageSize));
		}
		catch(Exception ex) { return BadRequest(ex.Message); }
	}

	[HttpPut("password")]
	[AllowAnonymous]
	public async Task<IActionResult> ChangePasswordAsync(PasswordChangeDto dto)
	{
		try
		{
			return Ok(await _profServ.ChangePasswordAsync(dto));
		}
		catch(Exception ex) { return BadRequest(ex.Message); }
	}

	[HttpPut("reset/{email}")]
	[AllowAnonymous]
	public async Task<IActionResult> ResetPasswordAsync(string email)
	{
		try
		{
			return Ok(await _profServ.ResetPasswordAsync(email));
		}
		catch(Exception ex) { return BadRequest(ex.Message); }
	}

	[HttpPatch("confirm")]
	[AllowAnonymous]
	public async Task<IActionResult> ConfirmEmailAsync(EmailConfirmDto dto)
	{
		try
		{
			return Ok(await _profServ.ConfirmEmailAsync(dto));
		}
		catch(Exception ex) { return BadRequest(ex.Message); }
	}

	[HttpGet("confirm/{email}")]
	[AllowAnonymous]
	public async Task<IActionResult> RequestConfirmEmailAsync(string email)
	{
		try
		{
			return Ok(await _profServ.RequestEmailConfirmationAsync(email));
		}
		catch(Exception ex) { return BadRequest(ex.Message); }
	}
}
