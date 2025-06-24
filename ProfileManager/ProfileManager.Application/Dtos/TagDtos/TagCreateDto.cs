using System.ComponentModel.DataAnnotations;

namespace ProfileManager.Application.Dtos.TagDtos;

public class TagCreateDto
{
	[Length(3, 30)]
	public string Name { get; set; }
	public string? Description { get; set; }

	public TagCreateDto(string name, string? description = null)
	{
		Name = name;
		Description = description;
	}
}
