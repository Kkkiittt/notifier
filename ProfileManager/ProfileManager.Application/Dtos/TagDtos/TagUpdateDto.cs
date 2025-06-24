namespace ProfileManager.Application.Dtos.TagDtos;

public class TagUpdateDto : TagCreateDto
{
	public long Id { get; set; }

	public TagUpdateDto(long id, string name, string? description = null) : base(name, description)
	{
		Id = id;
	}
}
