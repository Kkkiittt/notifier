namespace ProfileManager.Domain.Entities;

public class AuditableEntity
{
	public long Id { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime? UpdatedAt { get; set; }
}
