namespace ProfileManager.Domain.Entities;

public class Tag : AuditableEntity
{
	public string Name { get; set; }
	public string? Description { get; set; }

	public List<ProfileTag> ProfileTags { get; set; } = null!;

	public Tag(string name, string? description = null)
	{
		Name = name;
		Description = description;
		CreatedAt = DateTime.UtcNow;
	}
}
