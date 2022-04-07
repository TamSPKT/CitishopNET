using Microsoft.EntityFrameworkCore;

namespace CitishopNET.DataAccess.Models
{
	public class Invoice
	{
		public Guid Id { get; set; }
		[Precision(14, 2)]
		public decimal TotalCost { get; set; }

		public string UserId { get; set; }
		public ApplicationUser User { get; set; }

		public virtual List<InvoiceProduct> InvoiceProducts { get; set; }
	}
}
