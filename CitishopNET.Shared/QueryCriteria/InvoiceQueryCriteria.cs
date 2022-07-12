namespace CitishopNET.Shared.QueryCriteria
{
	public class InvoiceQueryCriteria : BaseQueryCriteria
	{
		public string UserId { get; set; } = string.Empty;
		public bool IgnoreUserId { get; set; } = false;
	}
}
