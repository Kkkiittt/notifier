using Microsoft.EntityFrameworkCore;

using ProfileManager.Application.Interfaces.Repositories;
using ProfileManager.Domain.Entities;
using ProfileManager.Infrastructure.Contexts;

namespace ProfileManager.Infrastructure.Repositories;

public class TagRepository : ITagRepository
{
	private readonly ProfileDbContext _db;

	public TagRepository(ProfileDbContext db)
	{
		_db = db;
	}

	public bool Create(Tag entity)
	{
		_db.Tags.Add(entity);
		return true;
	}

	public bool Delete(Tag entity)
	{
		_db.Tags.Remove(entity);
		return true;
	}

	public async Task<Tag?> GetAsync(long id)
	{
		return await _db.Tags.FindAsync(id);
	}

	public async Task<IEnumerable<Tag>> GetManyAsync(int skip, int take)
	{
		return await _db.Tags.Skip(skip).Take(take).ToListAsync();
	}

	public async Task<bool> SaveChangesAsync()
	{
		return await _db.SaveChangesAsync() > 0;
	}

	public bool Update(Tag entity)
	{
		_db.Tags.Update(entity);
		return true;
	}
}
