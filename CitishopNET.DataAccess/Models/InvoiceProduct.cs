using Microsoft.EntityFrameworkCore;

namespace CitishopNET.DataAccess.Models
{
	public class InvoiceProduct
	{
		public Guid InvoiceId { get; set; }
		public virtual Invoice Invoice { get; set; }

		public Guid ProductId { get; set; }
		public virtual Product Product { get; set; }

		[Precision(14, 2)]
		public decimal Cost { get; set; }

	}
}
