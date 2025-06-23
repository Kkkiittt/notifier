using UserManager.Domain.Entities;

namespace AdminManager.Application.Dtos;

public class AdminGetDto
{
	public long Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public DateTime Created { get; set; }
	public DateTime? Updated { get; set; }

	public static explicit operator AdminGetDto(Admin admin)
	{
		return new AdminGetDto
		{
			Id = admin.Id,
			Name = admin.Name,
			Email = admin.Email,
			Created = admin.CreatedAt,
			Updated = admin.UpdatedAt
		};
	}
}
