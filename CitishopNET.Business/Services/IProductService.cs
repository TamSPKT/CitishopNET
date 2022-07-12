using CitishopNET.Shared;
using CitishopNET.Shared.Dtos.Product;
using CitishopNET.Shared.QueryCriteria;

namespace CitishopNET.Business.Services
{
	public interface IProductService
	{
		PagedModel<ProductDto> GetProducts(ProductQueryCriteria criteria);
		Task<PagedModel<ProductDto>> GetRandomProductsAsync(int count);
		Task<ProductDto?> GetProductByIdAsync(Guid id);
		Task<ProductDto?> AddAsync(CreateProductDto createProductDto);
		Task<ProductDto?> UpdateAsync(Guid id, EditProductDto editProductDto);
		Task<ProductDto?> DeleteAsync(Guid id);
	}
}
