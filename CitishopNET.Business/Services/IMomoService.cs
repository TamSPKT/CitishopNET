using CitishopNET.Shared.MomoDtos;

namespace CitishopNET.Business.Services
{
	public interface IMomoService
	{
		public Task<MomoPaymentResponseDto?> SendPaymentRequestAsync(MomoPaymentRequestDto request);
	}
}
