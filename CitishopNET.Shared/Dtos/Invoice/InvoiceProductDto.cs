namespace CitishopNET.Shared.Dtos.Invoice
{
	public class InvoiceProductDto
	{
		public Guid InvoiceId { get; set; }
		public Guid ProductId { get; set; }
		public string Name { get; set; } = null!;
		public string ImageUrl { get; set; } = null!;
		public decimal CostPerItem { get; set; }
		public int Amount { get; set; }
		public decimal Cost { get; set; } // CostPerItem * Amount
	}
}
