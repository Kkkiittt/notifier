using UserManager.Domain.Entities;

namespace AdminManager.Application.Interfaces.Repositories;

public interface IAdminRepository
{
	public bool CreateAdmin(Admin admin);
	public bool UpdateAdmin(Admin admin);
	public bool DeleteAdmin(Admin admin);
	public Task<Admin?> GetAdminAsync(long id);
	public Task<Admin?> GetAdminAsync(string email);
	public Task<IEnumerable<Admin>> GetAdminsAsync(int skip, int take);
	public Task<bool> SaveChangesAsync();
}
