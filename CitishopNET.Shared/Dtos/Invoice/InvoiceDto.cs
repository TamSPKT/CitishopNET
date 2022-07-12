namespace CitishopNET.Shared.Dtos.Invoice
{
	public class InvoiceDto
	{
		public Guid Id { get; set; }
		public string UserId { get; set; } = null!;
		public DateTime DateOrdered { get; set; }
		public string PaymentStatus { get; set; } = null!; // PaymentStatusDto
		public decimal TotalPayment { get; set; } // TotalCost + TotalFee - Discount
	}
}
