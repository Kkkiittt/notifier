using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using AdminManager.Application.Interfaces.Services;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using Notifier.Shared.Enums;

using UserManager.Domain.Entities;

namespace AdminManager.Application.Services;

public class TokenService : ITokenService
{
	private readonly IConfiguration _config;

	public TokenService(IConfiguration config)
	{
		_config = config.GetSection("JWT");
	}

	public string CreateToken(Admin admin)
	{
		Claim[] claims =
		{
			new Claim(ClaimTypes.NameIdentifier, admin.Id.ToString()),
			new Claim(ClaimTypes.Role,(admin.SuperAdmin?Roles.SuperAdmin:Roles.Admin).ToString()),
			new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString()),
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
