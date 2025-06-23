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
			new Admin("Owner", "khamidov357@gmail.com", "$2a$11$ScBPsrMo1F5G5V.6T/DxtuaeRlerp.2HEe69nSWYWOTDYfShKSDiS")
			{
				SuperAdmin = true,
				CreatedAt = DateTime.MinValue,
				Id = 1
			});
		builder.HasKey(x => x.Id);
		builder.HasIndex(x => x.Email).IsUnique(true);
	}
}
