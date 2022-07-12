using System.ComponentModel;
using System.Reflection;

namespace CitishopNET.Shared.EnumDtos
{
	public enum DeliveryStatusDto
	{
		[Description("Chờ xác nhận")]
		WaitingForConfirmation = 0,

		[Description("Đang giao")]
		InDelivery = 1,

		[Description("Đã giao")]
		Delivered = 2,

		[Description("Đã huỷ")]
		Cancelled = 3,
	}

	public static class DeliveryStatusDtoExtension
	{
		public static string GetDescription(this DeliveryStatusDto value)
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
