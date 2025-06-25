using System.ComponentModel.DataAnnotations;

namespace ProfileManager.Application.Dtos.ProfileDtos;

public class PasswordChangeDto
{
	[Required]
	public long Id { get; set; }
	[Length(8, 30)]
	public string OldPassword { get; set; }
	[Length(8, 30)]
	public string NewPassword { get; set; }

	public PasswordChangeDto(long id, string oldPassword, string newPassword)
	{
		Id = id;
		OldPassword = oldPassword;
		NewPassword = newPassword;
	}
}
