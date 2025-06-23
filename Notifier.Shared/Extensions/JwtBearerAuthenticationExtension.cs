using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Notifier.Shared.Extensions;

public static class JwtBearerAuthenticationExtension
{
	public static IServiceCollection AddJwtBearerAuthentication(this IServiceCollection services, IConfiguration config)
	{
		config = config.GetSection("JWT");

		services.AddSwaggerGen(opt =>
		{
			opt.AddSecurityDefinition("Bearer", new()
			{
				Name = "Authorization",
				Type = SecuritySchemeType.ApiKey,
				Scheme = "Bearer",
				BearerFormat = "JWT",
				In = ParameterLocation.Header,
				Description = "JWT Authorization header using the Bearer scheme."
			});
			opt.AddSecurityRequirement(new OpenApiSecurityRequirement {
				{
					new OpenApiSecurityScheme {
						Reference = new OpenApiReference {
							Type = ReferenceType.SecurityScheme,
							Id = "Bearer"
						},
					},
					Array.Empty<string>()
				}
			});
		});

		services.AddAuthentication("Bearer").AddJwtBearer(opt =>
		{
			opt.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuer = false,
				ValidateAudience = false,
				ValidateIssuerSigningKey = false,
				IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(config["Key"] ?? throw new Exception("Key not found"))),
				ValidateLifetime = false
			};
		});
		services.AddAuthorization();

		return services;
	}
}
