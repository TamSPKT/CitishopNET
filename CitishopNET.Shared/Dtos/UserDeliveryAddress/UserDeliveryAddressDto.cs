namespace CitishopNET.Shared.Dtos.UserDeliveryAddress
{
	public class UserDeliveryAddressDto
	{
		public int Id { get; set; }
		public string UserId { get; set; } = null!;
		public string ReceiverName { get; set; } = null!;
		public string DeliveryAddress { get; set; } = null!;
		public string PhoneNumber { get; set; } = null!;
	}
}
