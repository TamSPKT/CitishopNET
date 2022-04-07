using CitishopNET.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CitishopNET.Business.Repository
{
	public class BaseRepository<T> : IBaseRepository<T> where T : class
	{
		private readonly ApplicationDbContext _dbContext;

		public BaseRepository(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		#region IBaseRepository<T> Members
		public DbSet<T> Entities => _dbContext.Set<T>();

		public EntityEntry<T> Entry(T entity) => _dbContext.Entry<T>(entity);

		public async Task<bool> AddAsync(T entity)
		{
			await _dbContext.Set<T>().AddAsync(entity);
			int n = await _dbContext.SaveChangesAsync();
			return n > 0;
		}

		public async Task<bool> DeleteAsync(T entity)
		{
			_dbContext.Set<T>().Remove(entity);
			int n = await _dbContext.SaveChangesAsync();
			return n > 0;
		}

		public async Task<bool> UpdateAsync(T entity)
		{
			_dbContext.Entry(entity).CurrentValues.SetValues(entity);
			int n = await _dbContext.SaveChangesAsync();
			return n > 0;
		}
		#endregion
	}
}
