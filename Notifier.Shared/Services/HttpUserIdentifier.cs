using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using Microsoft.AspNetCore.Http;

using Notifier.Shared.Enums;
using Notifier.Shared.Interfaces;

namespace Notifier.Shared.Services;

public class HttpUserIdentifier : IUserIdentifier
{
	private readonly IHttpContextAccessor _accessor;

	public HttpUserIdentifier(IHttpContextAccessor accessor)
	{
		_accessor = accessor;
	}

	public long Id
	{
		get
		{
			return long.Parse(_accessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new Exception("Invalid token"));
		}
	}

	public Roles Role
	{
		get
		{
			return Enum.Parse<Roles>(_accessor.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value ?? throw new Exception("Invalid token"));
		}
	}

	public bool IsAdmin
	{
		get
		{
			return Role == Roles.Admin || Role == Roles.SuperAdmin;
		}
	}

	public DateTime IssuedAt
	{
		get
		{
			return DateTimeOffset.FromUnixTimeSeconds(long.Parse(_accessor.HttpContext.User.FindFirst(JwtRegisteredClaimNames.Iat)?.Value ?? throw new Exception("Invalid token"))).UtcDateTime;
		}
	}
}
