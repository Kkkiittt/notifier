using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using ProfileManager.Application.Interfaces.Repositories;
using ProfileManager.Application.Interfaces.Services;
using ProfileManager.Application.Services;
using ProfileManager.Infrastructure.Contexts;
using ProfileManager.Infrastructure.Repositories;
using ProfileManager.Infrastructure.Services;

namespace ProfileManager.Infrastructure.Extensions;

public static class ProfileManagerModuleExtension
{
	public static IServiceCollection AddProfileManagerModule(this IServiceCollection services, IConfiguration config)
	{
		services.AddDbContext<ProfileDbContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")));
		services.AddScoped<IProfileRepository, ProfileRepository>();
		services.AddScoped<IEmailService, EmailService>();
		services.AddScoped<IProfileService, ProfileService>();
		services.AddScoped<IProfileTokenService, ProfileTokenService>();
		services.AddScoped<ITagService, TagService>();
		services.AddScoped<INotificationService, NotificationService>();
		services.AddScoped<ITagRepository, TagRepository>();
		return services;
	}
}
