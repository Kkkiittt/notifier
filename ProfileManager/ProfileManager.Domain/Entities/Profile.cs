using ProfileManager.Domain.Enums;

namespace ProfileManager.Domain.Entities;

public class Profile : AuditableEntity
{
	public string Name { get; set; }
	public string Email { get; set; }
	public string? DeviceToken { get; set; }
	public string PasswordHash { get; set; }

	public Gender Gender { get; set; }
	public DateTime BirthDate { get; set; }
	public List<Platform> Platforms { get; set; }

	public DateTime? LastOnline { get; set; }
	public bool ReceiveAd { get; set; }
	public bool ReceiveEmails { get; set; }
	public bool ReceivePushNotifications { get; set; }

	public List<ProfileTag> ProfileTags { get; set; }

	public Profile(string name, string email, string passwordHash, Gender gender, DateTime birthDate, List<Platform>? platforms = null)
	{
		Platforms = platforms ?? [];
		Name = name;
		Email = email;
		PasswordHash = passwordHash;
		Gender = gender;
		BirthDate = birthDate;
		CreatedAt = DateTime.UtcNow;
		ReceiveAd = true;
		ReceiveEmails = true;
		ReceivePushNotifications = false;
	}
}
