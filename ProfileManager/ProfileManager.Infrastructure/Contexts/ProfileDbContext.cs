using Microsoft.EntityFrameworkCore;

using ProfileManager.Domain.Entities;
using ProfileManager.Infrastructure.Configurations;

namespace ProfileManager.Infrastructure.Contexts;

public class ProfileDbContext : DbContext
{
	private readonly IEntityTypeConfiguration<Profile>? _profConfig;
	private readonly IEntityTypeConfiguration<Tag>? _tagConfig;
	private readonly IEntityTypeConfiguration<ProfileTag>? _profTagConfig;

	public DbSet<Profile> Profiles { get; set; }
	public DbSet<Tag> Tags { get; set; }
	public DbSet<ProfileTag> ProfileTags { get; set; }

	public ProfileDbContext(DbContextOptions<ProfileDbContext> options) : base(options)
	{
		_profConfig = new ProfileConfiguration();
		_tagConfig = new TagConfiguration();
		_profTagConfig = new ProfileTagConfiguration();
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		if(_profConfig != null)
			modelBuilder.ApplyConfiguration(_profConfig);

		if(_tagConfig != null)
			modelBuilder.ApplyConfiguration(_tagConfig);

		if(_profTagConfig != null)
			modelBuilder.ApplyConfiguration(_profTagConfig);

		base.OnModelCreating(modelBuilder);
	}
}
