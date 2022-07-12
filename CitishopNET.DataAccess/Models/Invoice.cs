using CitishopNET.DataAccess.Enums;
using Microsoft.EntityFrameworkCore;

namespace CitishopNET.DataAccess.Models
{
	public class Invoice
	{
		public Guid Id { get; set; }
		public DateTime DateOrdered { get; set; }
		public DateTime? DateDelivered { get; set; }
		public PaymentType PaymentType { get; set; }
		public PaymentStatus PaymentStatus { get; set; }
		public DeliveryStatus DeliveryStatus { get; set; }
		public string ReceiverName { get; set; } = null!;
		public string PhoneNumber { get; set; } = null!;
		public string DeliveryAddress { get; set; } = null!;
		public string DeliveryDescription { get; set; } = null!;
		[Precision(14, 2)]
		public decimal TotalCost { get; set; }
		[Precision(14, 2)]
		public decimal TotalFee { get; set; }
		[Precision(14, 2)]
		public decimal Discount { get; set; }

		public string UserId { get; set; } = null!;
		public ApplicationUser User { get; set; } = null!;

		public virtual List<InvoiceProduct> InvoiceProducts { get; set; } = null!;
	}
}
