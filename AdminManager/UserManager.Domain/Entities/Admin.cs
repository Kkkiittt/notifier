namespace UserManager.Domain.Entities;

public class Admin
{
	public long Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public string PasswordHash { get; set; } = string.Empty;
	public bool SuperAdmin { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime? UpdatedAt { get; set; }

	public Admin(string name, string email, string passswordHash)
	{
		Name = name;
		Email = email;
		PasswordHash = passswordHash;
		CreatedAt = DateTime.UtcNow;
		SuperAdmin = false;
	}
}
