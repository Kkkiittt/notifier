using Microsoft.EntityFrameworkCore;

using UserManager.Domain.Entities;

namespace AdminManager.Infrastructure.Contexts;

public class AdminDbContext : DbContext
{
	private readonly IEntityTypeConfiguration<Admin>? _adminConfig;

	public DbSet<Admin> Admins { get; set; }

	public AdminDbContext(DbContextOptions<AdminDbContext> options) : base(options)
	{
		_adminConfig = null;
	}
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		if(_adminConfig is not null)
			modelBuilder.ApplyConfiguration(_adminConfig);
		base.OnModelCreating(modelBuilder);
	}
}
