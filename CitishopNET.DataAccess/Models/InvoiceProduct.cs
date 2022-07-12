using Microsoft.EntityFrameworkCore;

namespace CitishopNET.DataAccess.Models
{
	public class InvoiceProduct
	{
		public Guid InvoiceId { get; set; }
		public virtual Invoice Invoice { get; set; } = null!;

		public Guid ProductId { get; set; }
		public virtual Product Product { get; set; } = null!;

		[Precision(14, 2)]
		public decimal CostPerItem { get; set; }
		public int Amount { get; set; }
	}
}
