using ProfileManager.Domain.Entities;

namespace ProfileManager.Application.Interfaces.Services;

public interface ITokenService
{
	public string CreateToken(Profile profile);
}
