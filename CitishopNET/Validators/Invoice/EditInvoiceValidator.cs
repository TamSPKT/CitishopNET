using CitishopNET.Shared.Dtos.Invoice;
using FluentValidation;

namespace CitishopNET.Validators.Invoice
{
	/// <summary>
	/// Validator for EditInvoiceDto.
	/// <para></para>
	/// Don't need to create validators in controllers as 
	/// RegisterValidatorsFromAssembly(...) in Program.cs 
	/// registers all validators derived from AbstractValidator within this assembly.
	/// </summary>
	public class EditInvoiceValidator : AbstractValidator<EditInvoiceDto>
	{
		public EditInvoiceValidator()
		{
			RuleFor(x => x.DeliveryStatus)
				.NotNull()
				.WithMessage("Không được để trống");
			RuleFor(x => x.DeliveryDescription)
				.MaximumLength(5000)
				.WithMessage("Quá giới hạn 5000 ký tự");
		}
	}
}
