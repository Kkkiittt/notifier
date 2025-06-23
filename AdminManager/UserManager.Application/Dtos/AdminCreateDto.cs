namespace AdminManager.Application.Dtos;

public class AdminCreateDto
{
	public string Name { get; set; }
	public string Email { get; set; }
	public string Password { get; set; }

	public AdminCreateDto(string name, string email, string password)
	{
		Name = name;
		Email = email;
		Password = password;
	}
}
