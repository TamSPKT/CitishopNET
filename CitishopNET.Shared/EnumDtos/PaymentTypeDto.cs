using System.ComponentModel;
using System.Reflection;

namespace CitishopNET.Shared.EnumDtos
{
	public enum PaymentTypeDto
	{
		[Description("Thanh toán khi nhận")]
		OnDelivery = 0, // Pay on delivery

		[Description("Thanh toán với ví Momo")]
		MomoWallet = 1, // Pay with Momo wallet
	}

	public static class PaymentTypeDtoExtension
	{
		public static string GetDescription(this PaymentTypeDto value)
		{
			if (value.GetType().GetField(value.ToString()) is FieldInfo fieldInfo)
			{
				var attribute = fieldInfo.GetCustomAttribute<DescriptionAttribute>();
				return attribute?.Description ?? string.Empty;
			}
			return string.Empty;
		}
	}
}
