using Notifier.Shared.Enums;

namespace Notifier.Shared.Interfaces;

public interface IUserIdentifier
{
	public long Id { get; }

	public Roles Role { get; }

	public bool IsAdmin { get; }

	public DateTime IssuedAt { get; }
}
