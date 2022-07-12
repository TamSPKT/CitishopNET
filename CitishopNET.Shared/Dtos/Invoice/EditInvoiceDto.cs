using CitishopNET.Shared.EnumDtos;

namespace CitishopNET.Shared.Dtos.Invoice
{
	public class EditInvoiceDto
	{
		public DeliveryStatusDto DeliveryStatus { get; set; }
		public string DeliveryDescription { get; set; } = null!;
	}
}
