namespace ProfileManager.Application.Dtos.TagDtos;

public class TagGetDto
{
	public long Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public DateTime Created { get; set; }
	public DateTime? Updated { get; set; }

	public TagGetDto(long id, string name, string description, DateTime created, DateTime? updated = null)
	{
		Id = id;
		Name = name;
		Description = description;
		Created = created;
		Updated = updated;
	}
}
