namespace CitishopNET.Business.Options
{
	public class MomoPaymentOptions
	{
		public const string MomoPayment = "MomoPayment";

		public string? PartnerCode { get; set; }
		public string? AccessKey { get; set; }
		public string? SecretKey { get; set; }
		public string? ApiEndpoint { get; set; }
	}
}
