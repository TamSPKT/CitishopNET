using AutoMapper;
using CitishopNET.Business.Extensions;
using CitishopNET.Business.Repository;
using CitishopNET.DataAccess.Models;
using CitishopNET.Shared;
using CitishopNET.Shared.Dtos.Product;
using CitishopNET.Shared.QueryCriteria;
using Microsoft.EntityFrameworkCore;

namespace CitishopNET.Business.Services
{
	public class ProductService : IProductService
	{
		private readonly IBaseRepository<Product> _productRepository;
		private readonly IMapper _mapper;

		public ProductService(IBaseRepository<Product> productRepository, IMapper mapper)
		{
			_productRepository = productRepository;
			_mapper = mapper;
		}

		public PagedModel<ProductDto> GetProducts(ProductQueryCriteria criteria)
		{
			var query = _productRepository.Entities.AsNoTracking().OrderBy(x => x.Name);
			var dtoQuery = query.Select(x => _mapper.Map<ProductDto>(x));
			var pagedProducts = dtoQuery.AsEnumerable()
				.Where(x => x.Name.Contains(criteria.Name, StringComparison.InvariantCultureIgnoreCase))
				.Paginate(criteria.Page, criteria.Limit);
			return pagedProducts;
		}

		public async Task<ProductDto?> GetProductByIdAsync(Guid id)
		{
			var query = _productRepository.Entities.AsNoTracking();
			var product = await query.SingleOrDefaultAsync(x => x.Id == id);
			return product != null
				? _mapper.Map<ProductDto>(product)
				: null;
		}

		public async Task<ProductDto?> AddAsync(CreateProductDto createProductDto)
		{
			var product = _mapper.Map<Product>(createProductDto);
			return await _productRepository.AddAsync(product)
				? _mapper.Map<ProductDto>(product)
				: null;
		}

		public async Task<ProductDto?> UpdateAsync(Guid id, EditProductDto editProductDto)
		{
			var product = await _productRepository.Entities.SingleOrDefaultAsync(x => x.Id == id);
			if (product == null)
			{
				return null;
			}
			product = _mapper.Map<EditProductDto, Product>(editProductDto, product);
			await _productRepository.UpdateAsync(product);
			return _mapper.Map<ProductDto>(product);
		}

		public async Task<ProductDto?> DeleteAsync(Guid id)
		{
			var product = await _productRepository.Entities.FirstOrDefaultAsync(x => x.Id == id);
			if (product == null)
			{
				return null;
			}
			await _productRepository.DeleteAsync(product);
			return _mapper.Map<ProductDto>(product);
		}
	}
}
