using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ProfileManager.Domain.Entities;

namespace ProfileManager.Infrastructure.Configurations;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
	public void Configure(EntityTypeBuilder<Tag> builder)
	{
		builder.HasKey(t => t.Id);
		builder.HasMany(t => t.ProfileTags).WithOne(p => p.Tag).HasForeignKey(p => p.TagId);
	}
}
