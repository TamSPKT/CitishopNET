namespace CitishopNET.Shared.Dtos.UserDeliveryAddress
{
	public class CreateUserDeliveryAddressDto
	{
		public string UserId { get; set; } = null!;
		public string ReceiverName { get; set; } = null!;
		public string DeliveryAddress { get; set; } = null!;
		public string PhoneNumber { get; set; } = null!;
	}
}
