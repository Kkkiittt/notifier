using AdminManager.Application.Dtos;
using AdminManager.Application.Interfaces.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminManager.Api.Controllers;
[ApiController]
[Route("admins")]
public class AdminController : ControllerBase
{
	private readonly IAdminService _adminService;
	public AdminController(IAdminService adminService)
	{
		_adminService = adminService;
	}

	[HttpPost("login")]
	[AllowAnonymous]
	public async Task<IActionResult> LoginAsync(AdminLoginDto dto)
	{
		try
		{
			return Ok(await _adminService.LoginAsync(dto));
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpPost("register")]
	[Authorize(Roles = "SuperAdmin")]
	public async Task<IActionResult> RegisterAsync(AdminCreateDto dto)
	{
		try
		{
			return Ok(await _adminService.RegisterAsync(dto));
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpPut("update")]
	[Authorize(Roles = "Admin, SuperAdmin")]
	public async Task<IActionResult> UpdateAsync(AdminUpdateDto dto)
	{
		try
		{
			return Ok(await _adminService.UpdateAsync(dto));
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpGet("update")]
	[Authorize(Roles = "Admin, SuperAdmin")]
	public async Task<IActionResult> GetTemplateAsync()
	{
		try
		{
			return Ok(await _adminService.GetTemplateAsync());
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpDelete("delete")]
	[Authorize(Roles = "Admin, SuperAdmin")]
	public async Task<IActionResult> DeleteAsync(long id)
	{
		try
		{
			return Ok(await _adminService.DeleteAsync(id));
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpGet("{id}")]
	[Authorize(Roles ="Admin, SuperAdmin")]
	public async Task<IActionResult> GetAsync(long id)
	{
		try
		{
			return Ok(await _adminService.GetAdminAsync(id));
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpGet]
	[Authorize(Roles = "Admin, SuperAdmin")]
	public async Task<IActionResult> GetManyAsync(int page = 1, int pageSize = 10)
	{
		try
		{
			return Ok(await _adminService.GetAdminsAsync(page, pageSize));
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}
}
