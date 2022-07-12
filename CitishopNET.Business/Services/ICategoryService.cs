using CitishopNET.Shared;
using CitishopNET.Shared.Dtos.Category;
using CitishopNET.Shared.QueryCriteria;

namespace CitishopNET.Business.Services
{
	public interface ICategoryService
	{
		Task<PagedModel<CategoryDto>> GetCategoriesAsync(BaseQueryCriteria criteria);
		Task<CategoryProductsDto?> GetCategoryByIdAsync(Guid id);
		Task<CategoryDto?> AddAsync(CreateCategoryDto createCategoryDto);
		Task<CategoryDto?> UpdateAsync(Guid id, EditCategoryDto editCategoryDto);
		Task<CategoryDto?> DeleteAsync(Guid id); 
	}
}
