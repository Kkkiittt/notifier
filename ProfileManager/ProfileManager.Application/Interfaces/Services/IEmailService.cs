namespace ProfileManager.Application.Interfaces.Services;

public interface IEmailService
{
	public Task SendEmailsAsync(string[] emails, string subject, string message);

	public Task SendEmailAsync(string email, string subject, string message);
}
