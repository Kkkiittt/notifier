using UserManager.Domain.Entities;

namespace AdminManager.Application.Interfaces.Services;

public interface IAdminTokenService
{
	public string CreateToken(Admin admin);
}
