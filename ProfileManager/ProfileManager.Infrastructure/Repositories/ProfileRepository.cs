using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using ProfileManager.Application.Interfaces.Repositories;
using ProfileManager.Domain.Entities;
using ProfileManager.Infrastructure.Contexts;

namespace ProfileManager.Infrastructure.Repositories;

public class ProfileRepository : IProfileRepository
{
	private readonly ProfileDbContext _db;

	public ProfileRepository(ProfileDbContext db)
	{
		_db = db;
	}

	public bool IncludeTagWeights { private get; set; }
	public bool IncludeTagValues { private get; set; }

	private IQueryable<Profile> IncludedProfiles
	{
		get
		{
			if(IncludeTagWeights)
				return _db.Profiles.Include(p => p.ProfileTags);
			else if(IncludeTagValues)
				return _db.Profiles.Include(p => p.ProfileTags).ThenInclude(pt => pt.Tag);
			else
				return _db.Profiles;
		}
	}

	public bool Create(Profile entity)
	{
		_db.Profiles.Add(entity);
		return true;
	}

	public bool Delete(Profile entity)
	{
		_db.Profiles.Remove(entity);
		return true;
	}

	public async Task<Profile?> GetAsync(string email)
	{
		return await IncludedProfiles.FirstOrDefaultAsync(p => p.Email == email);
	}

	public async Task<Profile?> GetAsync(long id)
	{
		if(IncludeTagWeights || IncludeTagValues)
			return await IncludedProfiles.FirstOrDefaultAsync(p => p.Id == id);
		else
			return await _db.Profiles.FindAsync(id);
	}

	public async Task<IEnumerable<Profile>> GetFilteredAsync(params Expression<Func<Profile, bool>>[] predicates)
	{
		var query = IncludedProfiles;

		foreach(var predicate in predicates)
		{
			query = query.Where(predicate);
		}

		return await query.ToListAsync();
	}

	public async Task<IEnumerable<Profile>> GetManyAsync(int skip, int take)
	{
		return await IncludedProfiles.Skip(skip).Take(take).ToListAsync();
	}

	public async Task<bool> SaveChangesAsync()
	{
		return await _db.SaveChangesAsync() > 0;
	}

	public bool Update(Profile entity)
	{
		_db.Profiles.Update(entity);
		return true;
	}
}