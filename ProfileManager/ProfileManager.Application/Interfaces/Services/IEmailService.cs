namespace ProfileManager.Application.Interfaces.Services;

public interface IEmailService
{
	public Task<bool> SendEmailAsync(string[] emails, string subject, string message);
}
