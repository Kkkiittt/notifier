using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using ProfileManager.Application.Dtos.TagDtos;
using ProfileManager.Application.Interfaces.Services;

namespace ProfileManager.Api.Controllers;

[ApiController]
[Route("tags")]
public class TagController : ControllerBase
{
	private readonly ITagService _tagServ;

	public TagController(ITagService tagServ)
	{
		_tagServ = tagServ;
	}

	[HttpPost]
	[Authorize(Roles = "Admin, SuperAdmin")]
	public async Task<IActionResult> CreateTagAsync(TagCreateDto dto)
	{
		try
		{
			return Ok(await _tagServ.CreateTagAsync(dto));
		}
		catch(Exception ex) { return BadRequest(ex.Message); }
	}

	[HttpPut]
	[Authorize(Roles = "Admin, SuperAdmin")]
	public async Task<IActionResult> UpdateTagAsync(TagUpdateDto dto)
	{
		try
		{
			return Ok(await _tagServ.UpdateTagAsync(dto));
		}
		catch(Exception ex) { return BadRequest(ex.Message); }
	}

	[HttpDelete("{id}")]
	[Authorize(Roles = "Admin, SuperAdmin")]
	public async Task<IActionResult> DeleteTagAsync(long id)
	{
		try
		{
			return Ok(await _tagServ.DeleteTagAsync(id));
		}
		catch(Exception ex) { return BadRequest(ex.Message); }
	}

	[HttpGet("{id}")]
	[Authorize(Roles = "Admin, SuperAdmin")]
	public async Task<IActionResult> GetTagAsync(long id)
	{
		try
		{
			return Ok(await _tagServ.GetTagAsync(id));
		}
		catch(Exception ex) { return BadRequest(ex.Message); }
	}

	[HttpGet]
	[Authorize(Roles = "Admin, SuperAdmin")]
	public async Task<IActionResult> GetAllTagsAsync(int page = 1, int pageSize = 50)
	{
		try
		{
			return Ok(await _tagServ.GetTagsAsync(page, pageSize));
		}
		catch(Exception ex) { return BadRequest(ex.Message); }
	}
}
