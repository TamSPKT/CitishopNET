namespace CitishopNET.Shared.QueryCriteria
{
	public class UserDeliveryAddressQueryCriteria : BaseQueryCriteria
	{
		public string UserId { get; set; } = string.Empty;
		public bool IgnoreUserId { get; set; } = false;
	}
}
