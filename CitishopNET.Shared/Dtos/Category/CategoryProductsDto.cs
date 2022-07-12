using CitishopNET.Shared.Dtos.Product;

namespace CitishopNET.Shared.Dtos.Category
{
	public class CategoryProductsDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; } = null!;
		public List<ProductDto> Products { get; set; } = null!;
	}
}
