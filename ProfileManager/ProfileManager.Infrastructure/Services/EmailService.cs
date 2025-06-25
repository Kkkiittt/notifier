
using System.Net.Mail;

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;

using ProfileManager.Application.Interfaces.Services;

namespace ProfileManager.Infrastructure.Services;

public class EmailService : IEmailService
{
	private readonly SmtpClient client;
	private readonly string FromMail;

	public EmailService(IConfiguration configuration)
	{
		FromMail = configuration["Email:Address"] ?? throw new Exception("NO Address");
		client = new SmtpClient(configuration["Email:Host"])
		{
			Credentials = new System.Net.NetworkCredential(configuration["Email:Address"], configuration["Email:Password"]),
			Port = int.Parse(configuration["Email:Port"] ?? throw new Exception("NO Port")),
			EnableSsl = true
		};
	}

	public async Task SendEmailAsync(string email, string subject, string message)
	{
		var msg = new MailMessage(
			from: FromMail,
			to: email,
			subject: subject,
			body: message
		);
		msg.IsBodyHtml = true;

		await client.SendMailAsync(msg);
	}

	public async Task SendEmailsAsync(string[] emails, string subject, string message)
	{
		if(emails.Length < 1 || emails.Length > 80)
			throw new Exception("Emails count must be between 1 and 80");

		var msg = new MailMessage();
		msg.From = new MailAddress(FromMail);
		msg.Subject = subject;
		msg.Body = message;
		msg.IsBodyHtml = true;

		foreach(var email in emails)
		{
			msg.Bcc.Add(new MailAddress(email));
		}

		await client.SendMailAsync(msg);
	}
}
