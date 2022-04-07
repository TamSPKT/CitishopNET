using CitishopNET.Shared;
using Microsoft.EntityFrameworkCore;

namespace CitishopNET.Business.Extensions
{
	public static class PaginationExtension
	{
		public static async Task<PagedModel<TModel>> PaginateAsync<TModel>(
			this IQueryable<TModel> query,
			int page,
			int limit,
			CancellationToken cancellationToken = default) where TModel : class
		{
			page = (page < 1) ? 1 : page;
			limit = (limit < 0) ? 0 : limit;

			int skipItems = (page - 1) * limit;
			var pagedItems = await query.Skip(skipItems).Take(limit).ToListAsync(cancellationToken);

			int totalItems = await query.CountAsync(cancellationToken);
			int totalPages = (int)Math.Ceiling(totalItems / (double)limit);

			return new PagedModel<TModel>
			{
				CurrentPage = page,
				Items = pagedItems,
				TotalItems = totalItems,
				TotalPages = totalPages,
			};
		}

		public static PagedModel<TModel> Paginate<TModel>(
			this IEnumerable<TModel> query,
			int page,
			int limit,
			CancellationToken cancellationToken = default) where TModel : class
		{
			page = (page < 1) ? 1 : page;
			limit = (limit < 0) ? 0 : limit;

			int skipItems = (page - 1) * limit;
			var pagedItems = query.Skip(skipItems).Take(limit).ToList();

			int totalItems = query.Count();
			int totalPages = (int)Math.Ceiling(totalItems / (double)limit);

			return new PagedModel<TModel>
			{
				CurrentPage = page,
				Items = pagedItems,
				TotalItems = totalItems,
				TotalPages = totalPages,
			};
		}
	}
}
