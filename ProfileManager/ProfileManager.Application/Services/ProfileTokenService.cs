using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using Notifier.Shared.Enums;

using ProfileManager.Application.Interfaces.Services;
using ProfileManager.Domain.Entities;

namespace ProfileManager.Application.Services;

public class ProfileTokenService : IProfileTokenService
{
	private readonly IConfiguration _config;

	public ProfileTokenService(IConfiguration config)
	{
		_config = config.GetSection("JWT");
	}

	public string CreateToken(Profile profile)
	{
		Claim[] claims =
		{
			new Claim(ClaimTypes.NameIdentifier, profile.Id.ToString()),
			new Claim(ClaimTypes.Role,(Roles.User.ToString())),
			new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString())
		};

		var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config["Key"] ?? throw new Exception("No key in config file")));
		var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		var token = new JwtSecurityToken(
			claims: claims,
			expires: DateTime.Now.AddDays(int.Parse(_config["Duration"] ?? throw new Exception("No duration in config file"))),
			signingCredentials: cred,
			issuer: _config["Issuer"],
			audience: _config["Audience"]
			);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}
}
