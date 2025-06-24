using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ProfileManager.Domain.Entities;

namespace ProfileManager.Infrastructure.Configurations;

public class ProfileTagConfiguration : IEntityTypeConfiguration<ProfileTag>
{
	public void Configure(EntityTypeBuilder<ProfileTag> builder)
	{
		builder.HasKey(pt => new { pt.ProfileId, pt.TagId });
		builder.HasIndex(pt => pt.TagId).IsUnique(false);

		builder.HasOne(pt => pt.Profile).WithMany(p => p.ProfileTags).HasForeignKey(pt => pt.ProfileId);
		builder.HasOne(pt => pt.Tag).WithMany(t => t.ProfileTags).HasForeignKey(pt => pt.TagId);
	}
}
