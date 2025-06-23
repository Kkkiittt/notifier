namespace AdminManager.Application.Dtos;

public class AdminLoginDto
{
	public string Email { get; set; }
	public string Password { get; set; }

	public AdminLoginDto(string email, string password)
	{
		Email = email;
		Password = password;
	}
}
