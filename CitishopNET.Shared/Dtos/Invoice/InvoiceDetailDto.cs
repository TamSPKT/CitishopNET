using CitishopNET.Shared.EnumDtos;

namespace CitishopNET.Shared.Dtos.Invoice
{
	public class InvoiceDetailDto
	{
		public Guid Id { get; set; }
		public string UserId { get; set; } = null!;
		public DateTime DateOrdered { get; set; }
		public DateTime? DateDelivered { get; set; }
		public string PaymentType { get; set; } = null!; // PaymentTypeDto
		public string PaymentStatus { get; set; } = null!; // PaymentStatusDto
		public string DeliveryStatus { get; set; } = null!; // DeliveryStatusDto
		public string DeliveryAddress { get; set; } = null!;
		public decimal TotalCost { get; set; }
		public decimal TotalFee { get; set; }
		public decimal Discount { get; set; }
		public decimal TotalPayment { get; set; } // TotalCost + TotalFee - Discount
		public List<InvoiceProductDto> InvoiceProducts { get; set; } = null!;
	}
}
