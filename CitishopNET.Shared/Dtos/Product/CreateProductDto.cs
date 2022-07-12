namespace CitishopNET.Shared.Dtos.Product
{
	public class CreateProductDto
	{
		public string Name { get; set; } = null!;
		public decimal Price { get; set; }
		public decimal? DiscountPrice { get; set; }
		public int Quantity { get; set; }
		public string Description { get; set; } = null!;
		public string ImageUrl { get; set; } = null!;
		public Guid CategoryId { get; set; }
	}
}
