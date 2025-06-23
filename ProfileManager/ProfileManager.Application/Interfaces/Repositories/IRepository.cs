using ProfileManager.Domain.Entities;

namespace ProfileManager.Application.Interfaces.Repositories;

public interface IRepository<T> where T : AuditableEntity
{
	public bool Create(T entity);
	public bool Update(T entity);
	public bool Delete(T entity);

	public Task<T> GetAsync(long id);
	public Task<IEnumerable<T>> GetManyAsync(int skip, int take);

	public Task<bool> SaveChangesAsync();
}
