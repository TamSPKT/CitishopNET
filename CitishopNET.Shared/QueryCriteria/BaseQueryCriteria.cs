namespace CitishopNET.Shared.QueryCriteria
{
	public class BaseQueryCriteria
	{
		public virtual int Limit { get; set; } = 10;
		public virtual int Page { get; set; } = 1;
	}
}
