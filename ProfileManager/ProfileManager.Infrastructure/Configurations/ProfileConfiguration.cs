using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ProfileManager.Domain.Entities;

namespace ProfileManager.Infrastructure.Configurations;

public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
{
	public void Configure(EntityTypeBuilder<Profile> builder)
	{
		builder.HasKey(p => p.Id);
		builder.HasIndex(p=>p.Email).IsUnique();
		builder.HasMany(p=>p.ProfileTags).WithOne(p=>p.Profile).HasForeignKey(p=>p.ProfileId);
	}
}
