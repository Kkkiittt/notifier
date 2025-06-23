using ProfileManager.Domain.Entities;

namespace ProfileManager.Application.Interfaces.Repositories;

public interface IProfileRepository : IRepository<Profile>
{
	public Task<Profile> GetAsync(string email);

	public Task<IEnumerable<Profile>> GetFilteredAsync(params Predicate<Profile>[] predicates);
}
