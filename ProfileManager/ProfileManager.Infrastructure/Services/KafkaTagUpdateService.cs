using System.Text.Json;

using Confluent.Kafka;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using ProfileManager.Application.Dtos.ProfileTagDtos;
using ProfileManager.Application.Interfaces.Repositories;
using ProfileManager.Domain.Entities;

namespace ProfileManager.Infrastructure.Services;

public class KafkaTagUpdateService : IHostedService
{
	private readonly ConsumerConfig _consConfig;
	private IConsumer<Ignore, string> _consumer = null!;
	private readonly IConfiguration _config;
	private readonly IServiceProvider _services;

	public KafkaTagUpdateService(IConfiguration config, IServiceProvider services)
	{
		_services = services;
		_config = config.GetSection("Kafka");

		_consConfig = new ConsumerConfig()
		{
			GroupId = _config["GroupId"],
			BootstrapServers = _config["Server"],
			AutoOffsetReset = AutoOffsetReset.Earliest
		};
	}

	public Task StartAsync(CancellationToken cancellationToken)
	{
		_consumer = new ConsumerBuilder<Ignore, string>(_consConfig).Build();
		_consumer.Subscribe(_config["Topic"]);

		Task.Run(() => ConsumeMessages(cancellationToken), cancellationToken);
		return Task.CompletedTask;
	}

	private async Task ConsumeMessages(CancellationToken token)
	{
		while(!token.IsCancellationRequested)
		{
			var consumeResult = _consumer.Consume(token);

			if(consumeResult != null)
			{
				await UpdatePlayer(consumeResult.Message.Value);
			}
		}

		_consumer.Close();
	}

	private async Task UpdatePlayer(string value)
	{
		try
		{
			using(var scope = _services.CreateScope())
			{
				var profRepo = scope.ServiceProvider.GetRequiredService<IProfileRepository>();
				var tagRepo = scope.ServiceProvider.GetRequiredService<ITagRepository>();
				ProfileTagCreateDto? dto = JsonSerializer.Deserialize<ProfileTagCreateDto>(value);

				if(dto is null)
					throw new Exception("Dto is null");


				profRepo.IncludeTagValues = true;
				Profile? profile = await profRepo.GetAsync(dto.ProfileId);

				if(profile is null)
					throw new Exception("Profile is null");

				ProfileTag? existingTag = profile.ProfileTags.FirstOrDefault(t => t.TagId == dto.TagId);
				if(existingTag is null)
				{
					Tag? tag = await tagRepo.GetAsync(dto.TagId);
					if(tag is null)
						throw new Exception("Tag is null");

					profile.ProfileTags.Add(new ProfileTag() { Tag = tag, TagId = tag.Id, Profile = profile, ProfileId = profile.Id, Weight = dto.Weight });
				}
				else
				{
					existingTag.Weight = dto.Weight;
				}

				profRepo.Update(profile);
				await profRepo.SaveChangesAsync();
			}
		}
		catch(Exception ex)
		{
			Console.WriteLine(ex.Message);
		}
	}

	public Task StopAsync(CancellationToken cancellationToken)
	{
		_consumer?.Close();
		_consumer?.Dispose();
		return Task.CompletedTask;
	}
}
