using AdminManager.Application.Dtos;

using UserManager.Domain.Entities;

namespace AdminManager.Application.Interfaces.Services;

public interface IAdminService
{
	public Task<string> LoginAsync(AdminLoginDto dto);
	public Task<bool> RegisterAsync(AdminCreateDto dto);

	public Task<bool> UpdateAsync(AdminUpdateDto dto);
	public Task<bool> DeleteAsync(long id);

	public Task<AdminGetDto> GetAdminAsync(long id);
	public Task<AdminGetDto> GetAdminAsync(string email);
	public Task<IEnumerable<AdminGetDto>> GetAdminsAsync(int page, int pageSize);
}
