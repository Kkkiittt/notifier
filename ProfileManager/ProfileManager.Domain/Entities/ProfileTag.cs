namespace ProfileManager.Domain.Entities;

public class ProfileTag
{
	public long ProfileId { get; set; }
	public Profile Profile { get; set; } = null!;

	public long TagId { get; set; }
	public Tag Tag { get; set; } = null!;

	public double Weight { get; set; }
}
