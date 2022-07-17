using CitishopNET.Shared;
using CitishopNET.Shared.Dtos.Invoice;
using CitishopNET.Shared.EnumDtos;
using CitishopNET.Shared.QueryCriteria;

namespace CitishopNET.Business.Services
{
	public interface IInvoiceService
	{
		Task<PagedModel<InvoiceDto>> GetInvoicesAsync(InvoiceQueryCriteria criteria);
		Task<InvoiceDetailDto?> GetInvoiceByIdAsync(Guid id);
		Task<InvoiceFullDetailDto?> GetFullInvoiceByIdAsync(Guid id);
		Task<(PaymentStatusDto, object)> AddAsync(CreateInvoiceDto dto, string returnUrl, string notifyUrl);
		Task<InvoiceDto?> UpdateAsync(Guid id, EditInvoiceDto dto);
		Task<bool> UpdatePaymentStatusAsync(Guid id, PaymentStatusDto dto);
		Task<InvoiceDto?> DeleteAsync(Guid id);
	}
}
