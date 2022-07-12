using AutoMapper;
using CitishopNET.Business.Extensions;
using CitishopNET.Business.Repository;
using CitishopNET.DataAccess.Models;
using CitishopNET.Shared;
using CitishopNET.Shared.Dtos.Category;
using CitishopNET.Shared.QueryCriteria;
using Microsoft.EntityFrameworkCore;

namespace CitishopNET.Business.Services
{
	public class CategoryService : ICategoryService
	{
		private readonly IBaseRepository<Category> _categoryRepository;
		private readonly IMapper _mapper;

		public CategoryService(IBaseRepository<Category> categoryRepository, IMapper mapper)
		{
			_categoryRepository = categoryRepository;
			_mapper = mapper;
		}

		public async Task<PagedModel<CategoryDto>> GetCategoriesAsync(BaseQueryCriteria criteria)
		{
			var query = _categoryRepository.Entities.AsNoTracking().OrderBy(x => x.Name);
			var dtoQuery = query.Select(x => _mapper.Map<CategoryDto>(x));
			var pagedCategories = await dtoQuery.PaginateAsync(criteria.Page, criteria.Limit);
			return pagedCategories;
		}

		public async Task<CategoryProductsDto?> GetCategoryByIdAsync(Guid id)
		{
			var query = _categoryRepository.Entities.Include(x => x.Products)
				.AsNoTracking();
			var category = await query.SingleOrDefaultAsync(x => x.Id == id);
			return category != null
				? _mapper.Map<CategoryProductsDto>(category)
				: null;
		}

		public async Task<CategoryDto?> AddAsync(CreateCategoryDto createCategoryDto)
		{
			var category = _mapper.Map<Category>(createCategoryDto);
			return await _categoryRepository.AddAsync(category)
				? _mapper.Map<CategoryDto>(category)
				: null;
		}

		public async Task<CategoryDto?> UpdateAsync(Guid id, EditCategoryDto editCategoryDto)
		{
			var category = await _categoryRepository.Entities.SingleOrDefaultAsync(x => x.Id == id);
			if (category == null)
			{
				return null;
			}
			category = _mapper.Map<EditCategoryDto, Category>(editCategoryDto, category);
			await _categoryRepository.UpdateAsync(category);
			return _mapper.Map<CategoryDto>(category);
		}

		public async Task<CategoryDto?> DeleteAsync(Guid id)
		{
			var category = await _categoryRepository.Entities.SingleOrDefaultAsync(x => x.Id == id);
			if (category == null)
			{
				return null;
			}
			await _categoryRepository.DeleteAsync(category);
			return _mapper.Map<CategoryDto>(category);
		}
	}
}
