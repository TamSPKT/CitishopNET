using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CitishopNET.Business.Repository
{
	public interface IBaseRepository<T> where T : class
	{
		DbSet<T> Entities { get; }
		EntityEntry<T> Entry(T entity);
		Task<bool> AddAsync(T entity);
		Task<bool> UpdateAsync(T entity);
		Task<bool> DeleteAsync(T entity);
	}
}
