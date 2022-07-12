using System.Text.Json.Serialization;

namespace CitishopNET.Shared.MomoDtos
{
	/// <summary>
	/// HTTP Request for Momo Payment Method
	/// <see href="https://developers.momo.vn/v2/#/docs/en/aio/?id=payment-method"/>
	/// </summary>
	public class MomoPaymentRequestDto
	{
		[JsonPropertyName("partnerCode")]
		public string PartnerCode { get; set; } = null!;

		[JsonPropertyName("accessKey")]
		public string AccessKey { get; set; } = null!;

		[JsonPropertyName("requestId")]
		public string RequestId { get; set; } = null!;

		[JsonPropertyName("amount")]
		public string Amount { get; set; } = null!;

		[JsonPropertyName("orderId")]
		public string OrderId { get; set; } = null!;

		[JsonPropertyName("orderInfo")]
		public string OrderInfo { get; set; } = null!;

		[JsonPropertyName("returnUrl")]
		public string ReturnUrl { get; set; } = null!;

		[JsonPropertyName("notifyUrl")]
		public string NotifyUrl { get; set; } = null!;


		[JsonPropertyName("requestType")]
		public string RequestType { get; set; } = "captureMoMoWallet";

		[JsonPropertyName("signature")]
		public string Signature { get; set; } = null!;

		[JsonPropertyName("extraData")]
		public string ExtraData { get; set; } = string.Empty;
	}
}
