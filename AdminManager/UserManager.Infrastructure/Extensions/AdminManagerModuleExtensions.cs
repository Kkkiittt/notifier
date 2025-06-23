using AdminManager.Application.Interfaces.Repositories;
using AdminManager.Application.Interfaces.Services;
using AdminManager.Application.Services;
using AdminManager.Infrastructure.Contexts;
using AdminManager.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdminManager.Infrastructure.Extensions;

public static class AdminManagerModuleExtensions
{
	public static IServiceCollection AddAdminManagerModule(this IServiceCollection services, IConfiguration config)
	{
		services.AddScoped<IAdminService, AdminService>();
		services.AddScoped<IAdminRepository, AdminRepository>();
		services.AddDbContext<AdminDbContext>(opt =>
		{
			opt.UseSqlServer(config.GetConnectionString("AdminDb"));
		});
		services.AddScoped<ITokenService, TokenService>();
		return services;
	}
}
