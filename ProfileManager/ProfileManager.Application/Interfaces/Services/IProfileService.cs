using ProfileManager.Application.Dtos.ProfileDtos;

namespace ProfileManager.Application.Interfaces.Services;

public interface IProfileService
{
	public Task<string> LoginAsync(ProfileLoginDto loginDto);

	public Task<bool> CreateProfileAsync(ProfileCreateDto createProfileDto);

	public Task<bool> UpdateProfileAsync(ProfileUpdateDto updateProfileDto);

	public Task<bool> DeleteProfileAsync(long id);

	public Task<ProfileShortGetDto> GetShortProfileAsync(long id);

	public Task<ProfileFullGetDto> GetFullProfileAsync(long id);

	public Task<List<ProfileShortGetDto>> GetShortProfilesAsync(int page, int pageSize);

	public Task<bool> ChangePasswordAsync(PasswordChangeDto dto);

	public Task<bool> ResetPasswordAsync(string email);

	public Task<bool> ConfirmEmailAsync(EmailConfirmDto dto);

	public Task<bool> RequestEmailConfirmationAsync(string email);
}
