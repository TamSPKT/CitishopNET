using System.ComponentModel;
using System.Reflection;

namespace CitishopNET.Shared.EnumDtos
{
	public enum PaymentStatusDto
	{
		[Description("Đang chờ thanh toán")]
		Waiting = 0, // Waiting for transaction

		[Description("Thanh toán thất bại")]
		Failed = 1, // Transaction failed

		[Description("Thanh toán thành công")]
		Succeeded = 2, // Transaction succeeded
	}

	public static class PaymentStatusDtoExtension
	{
		public static string GetDescription(this PaymentStatusDto value)
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
