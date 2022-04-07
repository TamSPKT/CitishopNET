namespace CitishopNET.Shared.Dtos.Product
{
	public class EditProductDto
	{
		public string Name { get; set; }
		public decimal Price { get; set; }
		public decimal? DiscountPrice { get; set; }
		public int Quantity { get; set; }
		public string Description { get; set; }
		public string ImageUrl { get; set; }
	}
}
