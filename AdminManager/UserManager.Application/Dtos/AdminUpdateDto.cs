namespace AdminManager.Application.Dtos;

public class AdminUpdateDto
{
	public long Id { get; set; }
	public string Name { get; set; }
	public string Email { get; set; }
	public string? Password { get; set; }

	public AdminUpdateDto(long id, string name, string email, string? password = null)
	{
		Id = id;
		Name = name;
		Email = email;
		Password = password;
	}
}
