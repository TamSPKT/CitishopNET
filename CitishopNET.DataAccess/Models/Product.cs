using Microsoft.EntityFrameworkCore;

namespace CitishopNET.DataAccess.Models
{
	public class Product
	{
		public Guid Id { get; set; }
		public string Name { get; set; } = null!;
		[Precision(14, 2)]
		public decimal Price { get; set; }
		[Precision(14, 2)]
		public decimal? DiscountPrice { get; set; }
		public int Quantity { get; set; }
		public string Description { get; set; } = null!;
		public string ImageUrl { get; set; } = null!;

		public Guid CategoryId { get; set; }
		public virtual Category Category { get; set; } = null!;

		public virtual List<InvoiceProduct> InvoiceProducts { get; set; } = null!;
	}
}
