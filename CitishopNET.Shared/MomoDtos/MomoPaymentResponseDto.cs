using System.Text.Json.Serialization;

namespace CitishopNET.Shared.MomoDtos
{
	/// <summary>
	/// HTTP Response for Momo Payment Method
	/// <see href="https://developers.momo.vn/v2/#/docs/en/aio/?id=payment-method"/>
	/// </summary>
	public class MomoPaymentResponseDto
	{
		[JsonPropertyName("requestId")]
		public string RequestId { get; set; } = null!;

		[JsonPropertyName("errorCode")]
		public int ErrorCode { get; set; }

		[JsonPropertyName("orderId")]
		public string OrderId { get; set; } = null!;

		[JsonPropertyName("message")]
		public string Message { get; set; } = null!;

		[JsonPropertyName("localMessage")]
		public string LocalMessage { get; set; } = null!;

		[JsonPropertyName("requestType")]
		public string RequestType { get; set; } = "captureMoMoWallet";

		[JsonPropertyName("payUrl")]
		public string PayUrl { get; set; } = null!;

		[JsonPropertyName("qrCodeUrl")]
		public string? QrCodeUrl { get; set; }

		[JsonPropertyName("deeplink")]
		public string? Deeplink { get; set; }

		[JsonPropertyName("deeplinkWebInApp")]
		public string? DeeplinkWebInApp { get; set; }

		[JsonPropertyName("signature")]
		public string Signature { get; set; } = null!;
	}
}
