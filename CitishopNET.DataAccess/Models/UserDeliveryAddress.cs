namespace CitishopNET.DataAccess.Models
{
	public class UserDeliveryAddress
	{
		public int Id { get; set; }

		public string UserId { get; set; } = null!;
		public ApplicationUser User { get; set; } = null!;

		public string ReceiverName { get; set; } = null!;
		public string DeliveryAddress { get; set; } = null!;
		public string PhoneNumber { get; set; } = null!;
	}
}
