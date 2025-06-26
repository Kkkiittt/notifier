
using Microsoft.Extensions.Caching.Memory;

using Notifier.Shared.Helpers;
using Notifier.Shared.Interfaces;

using ProfileManager.Application.Dtos.ProfileDtos;
using ProfileManager.Application.Interfaces.Repositories;
using ProfileManager.Application.Interfaces.Services;
using ProfileManager.Domain.Entities;

namespace ProfileManager.Application.Services;

public class ProfileService : IProfileService
{
	private readonly IProfileRepository _profileRepo;
	private readonly IUserIdentifier _userId;
	private readonly IProfileTokenService _tokenServ;
	private readonly IEmailService _emailServ;
	private readonly IMemoryCache _cache;

	public ProfileService(IProfileRepository profileRepo, IUserIdentifier userId, IProfileTokenService tokenServ, IMemoryCache cache, IEmailService emailServ)
	{
		_profileRepo = profileRepo;
		_userId = userId;
		_tokenServ = tokenServ;
		_cache = cache;
		_emailServ = emailServ;
	}

	public async Task<bool> ChangePasswordAsync(PasswordChangeDto dto)
	{
		Profile? profile = await _profileRepo.GetAsync(_userId.Id);
		if(profile is null)
			throw new Exception("Profile not found");

		if(!Hasher.Verify(dto.OldPassword, profile.PasswordHash))
			throw new Exception("Password is incorrect");

		profile.PasswordHash = Hasher.Hash(dto.NewPassword);

		_profileRepo.Update(profile);
		return await _profileRepo.SaveChangesAsync();
	}

	public async Task<bool> ConfirmEmailAsync(EmailConfirmDto dto)
	{
		Profile? profile = await _profileRepo.GetAsync(dto.Email);
		if(profile is null)
			throw new Exception("Profile not found");

		int code;
		if(!_cache.TryGetValue(dto.Email, out code))
			throw new Exception("Code expired");

		if(code != int.Parse(dto.Code))
			throw new Exception("Invalid code");

		profile.EmailConfirmed = true;

		_profileRepo.Update(profile);
		return await _profileRepo.SaveChangesAsync();
	}

	public async Task<bool> CreateProfileAsync(ProfileCreateDto dto)
	{
		Profile? emailProfile = await _profileRepo.GetAsync(dto.Email);
		if(emailProfile is not null)
			throw new Exception("Email is already in use");

		Profile profile = new Profile(dto.Name, dto.Email, Hasher.Hash(dto.Password), dto.Gender, dto.Birthdate, dto.Platforms);

		_profileRepo.Create(profile);
		return await _profileRepo.SaveChangesAsync();
	}

	public async Task<bool> DeleteProfileAsync(long id)
	{
		Profile? profile = await _profileRepo.GetAsync(id);
		if(profile is null)
			throw new Exception("Profile not found");

		if(profile.Id != _userId.Id && !_userId.IsAdmin)
			throw new Exception("You can't delete this profile");

		_profileRepo.Delete(profile);
		return await _profileRepo.SaveChangesAsync();
	}

	public async Task<ProfileFullGetDto> GetFullProfileAsync(long id)
	{
		_profileRepo.IncludeTagValues = true;
		Profile? profile = await _profileRepo.GetAsync(id);
		_profileRepo.IncludeTagValues = false;

		if(profile is null)
			throw new Exception("Profile not found");
		return (ProfileFullGetDto)profile;
	}

	public async Task<ProfileShortGetDto> GetShortProfileAsync(long id)
	{
		Profile? profile = await _profileRepo.GetAsync(id);
		if(profile is null)
			throw new Exception("Profile not found");

		if(profile.Id != _userId.Id && !_userId.IsAdmin)
			throw new Exception("You can't get this profile");

		return (ProfileShortGetDto)profile;
	}

	public async Task<List<ProfileShortGetDto>> GetShortProfilesAsync(int page, int pageSize)
	{
		if(page < 1 || pageSize < 1)
			throw new Exception("Invalid page or pageSize");

		if(pageSize > 50)
			pageSize = 50;

		int skip = (page - 1) * pageSize;
		var profiles = await _profileRepo.GetManyAsync(skip, pageSize);

		return profiles.Select(p => (ProfileShortGetDto)p).ToList();
	}

	public async Task<ProfileUpdateDto> GetTemplateAsync()
	{
		Profile? profile = await _profileRepo.GetAsync(_userId.Id);
		if(profile is null)
			throw new Exception("Profile not found");

		ProfileUpdateDto dto = new ProfileUpdateDto(profile.Id, profile.Name, profile.Email, profile.Gender, profile.BirthDate, profile.Platforms);

		return dto;
	}

	public async Task<string> LoginAsync(ProfileLoginDto loginDto)
	{
		Profile? profile = await _profileRepo.GetAsync(loginDto.Email);
		if(profile is null)
			throw new Exception("Invalid credentials");

		if(!Hasher.Verify(loginDto.Password, profile.PasswordHash))
			throw new Exception("Invalid credentials");

		if(!profile.EmailConfirmed)
			throw new Exception("Inactive account");

		return _tokenServ.CreateToken(profile);
	}

	public async Task<bool> RequestEmailConfirmationAsync(string email)
	{
		Profile? profile = await _profileRepo.GetAsync(email);
		if(profile is null)
			throw new Exception("Profile not found");

		if(profile.EmailConfirmed)
			throw new Exception("Email is already confirmed");

		int code = Random.Shared.Next(100000, 999999);

		_cache.Set(email, code, TimeSpan.FromMinutes(5));
		await _emailServ.SendEmailAsync(email, "Email confirmation", $"Your confirmation code for notifier is <strong>{code}<strong>.");

		return true;
	}

	public async Task<bool> ResetPasswordAsync(string email)
	{
		Profile? profile = await _profileRepo.GetAsync(email);
		if(profile is null)
			throw new Exception("Profile not found");

		string password = Guid.NewGuid().ToString("N").Substring(0, 10);

		profile.PasswordHash = Hasher.Hash(password);
		profile.UpdatedAt = DateTime.UtcNow;

		_profileRepo.Update(profile);
		if(!await _profileRepo.SaveChangesAsync())
			return false;

		await _emailServ.SendEmailAsync(email, "Password reset", $"Your new password for notifier is <strong>{password}<strong>.<br>Please change it as soon as possible.");

		return true;
	}

	public async Task<bool> UpdateProfileAsync(ProfileUpdateDto updateProfileDto)
	{
		Profile? profile = await _profileRepo.GetAsync(updateProfileDto.Id);
		Profile? emailProfile = await _profileRepo.GetAsync(updateProfileDto.Email);
		if(profile is null)
			throw new Exception("Profile not found");

		if(emailProfile is not null && emailProfile.Id != profile.Id)
			throw new Exception("Email is already in use");

		if(profile.Id != _userId.Id)
			throw new Exception("You can't update this profile");

		profile.Name = updateProfileDto.Name;
		profile.Email = updateProfileDto.Email;
		profile.Gender = updateProfileDto.Gender;
		profile.BirthDate = updateProfileDto.Birthdate;
		profile.Platforms = updateProfileDto.Platforms;
		profile.UpdatedAt = DateTime.UtcNow;

		_profileRepo.Update(profile);
		return await _profileRepo.SaveChangesAsync();
	}
}
