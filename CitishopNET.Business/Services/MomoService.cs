using CitishopNET.Business.Options;
using CitishopNET.Shared.MomoDtos;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace CitishopNET.Business.Services
{
	public class MomoService : IMomoService
	{
		private readonly MomoPaymentOptions _options;
		private readonly ILogger _logger;

		public MomoService(IOptions<MomoPaymentOptions> optionsAccessor, ILogger<MomoService> logger)
		{
			_options = optionsAccessor.Value;
			_logger = logger;
		}

		public async Task<MomoPaymentResponseDto?> SendPaymentRequestAsync(MomoPaymentRequestDto request)
		{
			request.PartnerCode = _options.PartnerCode!;
			request.AccessKey = _options.AccessKey!;

			request.SignSHA256(_options.SecretKey!);

			var jsonString = JsonSerializer.Serialize(request);
			_logger.LogInformation("Request to Momo: {Json}", jsonString);
			var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

			using var client = new HttpClient();
			var response = await client.PostAsync(_options.ApiEndpoint, stringContent);

			if (response.IsSuccessStatusCode)
			{
				var json = await response.Content.ReadAsStringAsync();
				_logger.LogInformation("Response from Momo: {Json}", json);
				return JsonSerializer.Deserialize<MomoPaymentResponseDto>(json);
			}

			_logger.LogInformation("Momo return {StatusCode}: {Content}", (int)response.StatusCode, await response.Content.ReadAsStringAsync());
			return null;
		}
	}

	public static class MomoPaymentExtension
	{
		public static MomoPaymentRequestDto SignSHA256(this MomoPaymentRequestDto value, string secretKey)
		{
			string rawHash = $"partnerCode={value.PartnerCode}"
				+ $"&accessKey={value.AccessKey}"
				+ $"&requestId={value.RequestId}"
				+ $"&amount={value.Amount}"
				+ $"&orderId={value.OrderId}"
				+ $"&orderInfo={value.OrderInfo}"
				+ $"&returnUrl={value.ReturnUrl}"
				+ $"&notifyUrl={value.NotifyUrl}"
				+ $"&extraData={value.ExtraData}";
			value.Signature = SignSHA256(rawHash, secretKey);
			return value;
		}

		public static bool CheckSHA256(this MomoPaymentResponseDto value, string secretKey)
		{
			string rawHash = $"requestId={value.RequestId}"
				+ $"&orderId={value.OrderId}"
				+ $"&message={value.Message}"
				+ $"&localMessage={value.LocalMessage}"
				+ $"&payUrl={value.PayUrl}"
				+ $"&errorCode={value.ErrorCode}"
				+ $"&requestType={value.RequestType}";
			string signature = SignSHA256(rawHash, secretKey);
			return signature == value.Signature;
		}

		public static string SignSHA256(string message, string key)
		{
			byte[] keyByte = Encoding.UTF8.GetBytes(key);
			byte[] messageBytes = Encoding.UTF8.GetBytes(message);

			using var hmacsha256 = new HMACSHA256(keyByte);
			byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
			string hex = BitConverter.ToString(hashmessage);
			hex = hex.Replace("-", "").ToLower();
			return hex;
		}
	}
}
