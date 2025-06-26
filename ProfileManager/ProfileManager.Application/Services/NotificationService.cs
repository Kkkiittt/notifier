
using System.Linq.Expressions;

using ProfileManager.Application.Dtos.NotificationDtos;
using ProfileManager.Application.Interfaces.Repositories;
using ProfileManager.Application.Interfaces.Services;
using ProfileManager.Domain.Entities;

namespace ProfileManager.Application.Services;

public class NotificationService : INotificationService
{
	private readonly IProfileRepository _profRepo;
	private readonly IEmailService _emailServ;

	public NotificationService(IProfileRepository profRepo, IEmailService emailServ)
	{
		_emailServ = emailServ;
		_profRepo = profRepo;
	}

	public async Task<int> SendFilteredNotificationAsync(FilteredNotificationDto dto)
	{
		List<Expression<Func<Profile, bool>>> expressions = new List<Expression<Func<Profile, bool>>>();

		expressions.Add(p => p.ReceiveAd);
		expressions.Add(p => p.EmailConfirmed);

		if(dto.AllowedGenders is not null)
			expressions.Add(p => dto.AllowedGenders.Contains(p.Gender));

		if(dto.MinimumAge is not null)
			expressions.Add(p => DateTime.UtcNow.Year - p.BirthDate.Year >= dto.MinimumAge.Value);

		if(dto.MaximumAge is not null)
			expressions.Add(p => DateTime.UtcNow.Year - p.BirthDate.Year <= dto.MaximumAge.Value);

		if(dto.NeededPlatforms is not null)
			expressions.Add(p => !p.Platforms.Except(dto.NeededPlatforms).Any());

		if(dto.MinimumLastOnline is not null)
			expressions.Add(p => p.LastOnline >= dto.MinimumLastOnline);

		if(dto.MaximumLastOnline is not null)
			expressions.Add(p => p.LastOnline <= dto.MaximumLastOnline);

		foreach(var tagReq in dto.TagRequirements ?? [])
		{
			if(tagReq.MinimumWeight is not null && tagReq.MaximumWeight is not null)
			{
				expressions.Add(p => p.ProfileTags.Any(pt => pt.TagId == tagReq.TagId && pt.Weight >= tagReq.MinimumWeight && pt.Weight <= tagReq.MaximumWeight));
			}
			else if(tagReq.MinimumWeight is not null)
			{
				expressions.Add(p => p.ProfileTags.Any(pt => pt.TagId == tagReq.TagId && pt.Weight >= tagReq.MinimumWeight));
			}
			else if(tagReq.MaximumWeight is not null)
			{
				expressions.Add(p => p.ProfileTags.Any(pt => pt.TagId == tagReq.TagId && pt.Weight <= tagReq.MaximumWeight));
			}
			else
			{
				expressions.Add(p => p.ProfileTags.Any(pt => pt.TagId == tagReq.TagId));
			}
		}
		expressions.Add(p => p.ReceiveEmails);
		IEnumerable<Profile> emailProfiles = await _profRepo.GetFilteredAsync(expressions.ToArray());
		expressions.RemoveAt(expressions.Count - 1);
		expressions.Add(p => p.ReceivePushNotifications);

		IEnumerable<Profile> pushProfiles = await _profRepo.GetFilteredAsync(expressions.ToArray());

		if(emailProfiles.Count() < 80)
		{
			await _emailServ.SendEmailsAsync(emailProfiles.Select(p => p.Email).ToArray(), dto.Title, dto.Message);
		}
		else
		{
			await SendBatchedEmailsAsync(emailProfiles, dto.Title, dto.Message);
		}
		//send pushes

		return /*pushProfiles.Count()*/ + emailProfiles.Count();
	}

	public async Task<int> SendMassNotificationAsync(MassNotificationDto dto)
	{
		IEnumerable<Profile> emailProfiles = await _profRepo.GetFilteredAsync(p => dto.ProfileIds.Contains(p.Id), p => p.ReceiveEmails, p => p.ReceiveAd, p => p.EmailConfirmed);
		IEnumerable<Profile> pushProfiles = await _profRepo.GetFilteredAsync(p => dto.ProfileIds.Contains(p.Id), p => p.ReceivePushNotifications, predicates => predicates.ReceiveAd, p => p.EmailConfirmed);

		if(emailProfiles.Count() < 80)
		{
			await _emailServ.SendEmailsAsync(emailProfiles.Select(p => p.Email).ToArray(), dto.Title, dto.Message);
		}
		else
		{
			await SendBatchedEmailsAsync(emailProfiles, dto.Title, dto.Message);
		}
		//send pushes

		return /*pushProfiles.Count()*/ +emailProfiles.Count();
	}

	private async Task SendBatchedEmailsAsync(IEnumerable<Profile> profiles, string title, string message, int batchSize = 80)
	{
		List<string[]> emailBatches = [];
		int batchCount = profiles.Count() / batchSize + 1;
		for(int i = 0; i < batchCount; i++)
		{
			emailBatches.Add(profiles.Skip(i * batchSize).Take(batchSize).Select(p => p.Email).ToArray());
		}

		Task[] tasks = emailBatches.Select(emails => _emailServ.SendEmailsAsync(emails, title, message)).ToArray();
		await Task.WhenAll(tasks);
	}
}
