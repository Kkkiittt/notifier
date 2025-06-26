
using AdminManager.Application.Dtos;
using AdminManager.Application.Interfaces.Repositories;
using AdminManager.Application.Interfaces.Services;

using Notifier.Shared.Enums;
using Notifier.Shared.Helpers;
using Notifier.Shared.Interfaces;

using UserManager.Domain.Entities;

namespace AdminManager.Application.Services;

public class AdminService : IAdminService
{
	private readonly IAdminRepository _adminRepo;
	private readonly IAdminTokenService _token;
	private readonly IUserIdentifier _userId;

	public AdminService(IAdminRepository adminRepo, IAdminTokenService token, IUserIdentifier userId)
	{
		_adminRepo = adminRepo;
		_token = token;
		_userId = userId;
	}

	public async Task<bool> DeleteAsync(long id)
	{
		Admin? admin = await _adminRepo.GetAdminAsync(id);
		if(admin is null)
			throw new Exception("Admin not found");

		if(_userId.Id != admin.Id && _userId.Role != Roles.SuperAdmin)
			throw new Exception("No permission");

		_adminRepo.DeleteAdmin(admin);
		return await _adminRepo.SaveChangesAsync();

	}

	public async Task<AdminGetDto> GetAdminAsync(long id)
	{
		Admin? admin = await _adminRepo.GetAdminAsync(id);
		if(admin is null)
			throw new Exception("Admin not found");

		if(_userId.Id != admin.Id && _userId.Role != Roles.SuperAdmin)
			throw new Exception("No permission");

		return (AdminGetDto)admin;
	}

	public async Task<IEnumerable<AdminGetDto>> GetAdminsAsync(int page, int pageSize)
	{
		if(_userId.Role != Roles.SuperAdmin)
			throw new Exception("No permission");

		if(page < 1 || pageSize < 1)
			throw new Exception("Invalid page arguments");

		if(pageSize > 50)
			pageSize = 50;

		int skip = (page - 1) * pageSize;
		IEnumerable<Admin> admins = await _adminRepo.GetAdminsAsync(skip, pageSize);

		return admins.Select(a => (AdminGetDto)a);
	}

	public async Task<AdminUpdateDto> GetTemplateAsync()
	{
		Admin? admin = await _adminRepo.GetAdminAsync(_userId.Id);
		if(admin is null)
			throw new Exception("Admin not found");

		return new AdminUpdateDto(admin.Id, admin.Name, admin.Email);
	}

	public async Task<string> LoginAsync(AdminLoginDto dto)
	{
		Admin? admin = await _adminRepo.GetAdminAsync(dto.Email);

		if(admin is null)
			throw new Exception("Invalid credentials");

		if(!Hasher.Verify(dto.Password, admin.PasswordHash))
			throw new Exception("Invalid credentials");

		return _token.CreateToken(admin);
	}

	public Task<bool> RegisterAsync(AdminCreateDto dto)
	{
		if(_userId.Role != Roles.SuperAdmin)
			throw new Exception("No permission");

		Admin admin = new Admin(dto.Name, dto.Email, Hasher.Hash(dto.Password));

		_adminRepo.CreateAdmin(admin);
		return _adminRepo.SaveChangesAsync();
	}

	public async Task<bool> UpdateAsync(AdminUpdateDto dto)
	{
		Admin? admin = await _adminRepo.GetAdminAsync(dto.Id);
		if(admin is null)
			throw new Exception("Admin not found");

		if(_userId.Id != admin.Id && _userId.Role != Roles.SuperAdmin)
			throw new Exception("No permission");

		admin.Name = dto.Name;
		admin.UpdatedAt= DateTime.UtcNow;
		admin.Email = dto.Email;
		if(dto.Password is not null)
		{
			if(dto.Password.Length < 6 || dto.Password.Length > 20)
				throw new Exception("Invalid password length");
			admin.PasswordHash = Hasher.Hash(dto.Password);
		}

		_adminRepo.UpdateAdmin(admin);
		return await _adminRepo.SaveChangesAsync();
	}
}
