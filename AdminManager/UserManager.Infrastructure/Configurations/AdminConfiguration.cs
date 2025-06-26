using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using UserManager.Domain.Entities;

namespace AdminManager.Infrastructure.Configurations;

public class AdminConfiguration : IEntityTypeConfiguration<Admin>
{
	public void Configure(EntityTypeBuilder<Admin> builder)
	{
		builder.HasData(
			new Admin("Owner", "khamidov357@gmail.com", "$2a$11$X46NFh.5VRZzPMljoZwPN.RtbxoopbjysG.LOIFkslMBA4Xu2J50m")
			{
				SuperAdmin = true,
				CreatedAt = DateTime.MinValue,
				Id = 1
			});
		builder.HasKey(x => x.Id);
		builder.HasIndex(x => x.Email).IsUnique(true);
	}
}
