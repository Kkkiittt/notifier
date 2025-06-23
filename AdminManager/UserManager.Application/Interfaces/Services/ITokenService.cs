using UserManager.Domain.Entities;

namespace AdminManager.Application.Interfaces.Services;

public interface ITokenService
{
	public string CreateToken(Admin admin);
}
