﻿using ProfileManager.Domain.Entities;

namespace ProfileManager.Application.Interfaces.Services;

public interface IProfileTokenService
{
	public string CreateToken(Profile profile);
}
