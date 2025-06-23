
using AdminManager.Application.Interfaces.Repositories;
using AdminManager.Infrastructure.Contexts;

using Microsoft.EntityFrameworkCore;

using UserManager.Domain.Entities;

namespace AdminManager.Infrastructure.Repositories;

public class AdminRepository : IAdminRepository
{
	private readonly AdminDbContext _dbContext;

	public AdminRepository(AdminDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public bool CreateAdmin(Admin admin)
	{
		_dbContext.Admins.Add(admin);
		return true;
	}

	public bool DeleteAdmin(Admin admin)
	{
		_dbContext.Admins.Remove(admin);
		return true;
	}

	public async Task<Admin?> GetAdminAsync(long id)
	{
		return await _dbContext.Admins.FindAsync(id);
	}

	public async Task<Admin?> GetAdminAsync(string email)
	{
		return await _dbContext.Admins.FirstOrDefaultAsync(x => x.Email == email);
	}

	public async Task<IEnumerable<Admin>> GetAdminsAsync(int skip, int take)
	{
		return await _dbContext.Admins.Skip(skip).Take(take).ToListAsync();
	}

	public async Task<bool> SaveChangesAsync()
	{
		return await _dbContext.SaveChangesAsync() > 0;
	}

	public bool UpdateAdmin(Admin admin)
	{
		_dbContext.Admins.Update(admin);
		return true;
	}
}
