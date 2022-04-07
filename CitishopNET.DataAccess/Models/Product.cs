using Microsoft.EntityFrameworkCore;

namespace CitishopNET.DataAccess.Models
{
	public class Product
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		[Precision(14, 2)]
		public decimal Price { get; set; }
		[Precision(14, 2)]
		public decimal? DiscountPrice { get; set; }
		public int Quantity { get; set; }
		public string Description { get; set; }
		public string ImageUrl { get; set; }

		public virtual List<InvoiceProduct> InvoiceProducts { get; set; }
	}
}
