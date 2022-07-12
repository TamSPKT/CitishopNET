using CitishopNET.Shared.Dtos.Cart;
using CitishopNET.Shared.EnumDtos;

namespace CitishopNET.Shared.Dtos.Invoice
{
	public class CreateInvoiceDto
	{
		// 1.Giỏ hàng
		public List<CartItemDto> CartItems { get; set; } = null!;
		public string DeliveryDescription { get; set; } = null!;
		// 2.Thông tin
		public string ReceiverName { get; set; } = null!;
		public string Email { get; set; } = null!;
		public string PhoneNumber { get; set; } = null!;
		public string DeliveryAddress { get; set; } = null!;
		// 3.Thanh toán
		public PaymentTypeDto PaymentType { get; set; }
		// CheckoutSummary
		public decimal TotalCost { get; set; }
		public decimal TotalFee { get; set; }
		public decimal Discount { get; set; }
		// Momo Payment
		public string ReturnUrl { get; set; } = null!;
	}
}
