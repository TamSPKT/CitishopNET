using CitishopNET.Shared.Dtos.Invoice;
using CitishopNET.Shared.EnumDtos;
using FluentValidation;

namespace CitishopNET.Validators.Invoice
{
	/// <summary>
	/// Validator for CreateInvoiceDto.
	/// <para></para>
	/// Don't need to create validators in controllers as 
	/// RegisterValidatorsFromAssembly(...) in Program.cs 
	/// registers all validators derived from AbstractValidator within this assembly.
	/// </summary>
	public class CreateInvoiceValidator : AbstractValidator<CreateInvoiceDto>
	{
		public CreateInvoiceValidator()
		{
			RuleForEach(x => x.CartItems).ChildRules(items =>
			{
				items.RuleFor(x => x.ProductId)
					.NotEmpty()
					.WithMessage("Không được để trống");
				items.RuleFor(x => x.Quantity)
					.NotEmpty()
					.WithMessage("Không được để trống hoặc bằng 0")
					.GreaterThan(0)
					.WithMessage("Phải lớn hơn 0");
			});
			RuleFor(x => x.DeliveryDescription)
				.MaximumLength(5000)
				.WithMessage("Quá giới hạn 5000 ký tự");
			RuleFor(x => x.ReceiverName)
				.NotEmpty()
				.WithMessage("Không được để trống");
			RuleFor(x => x.Email)
				.EmailAddress()
				.WithMessage("Email không hợp lệ");
			RuleFor(x => x.PhoneNumber)
				.NotEmpty()
				.WithMessage("Không được để trống")
				.Matches("^(84|0[3|5|7|8|9])+([0-9]{8})$")
				.WithMessage("Số điện thoại không hợp lệ");
			RuleFor(x => x.DeliveryAddress)
				.NotEmpty()
				.WithMessage("Không được để trống");
			RuleFor(x => x.PaymentType)
				.NotNull()
				.WithMessage("Không được để trống");
			RuleFor(x => x.TotalCost)
				.NotEmpty()
				.WithMessage("Không được để trống hoặc bằng 0")
				.GreaterThan(0)
				.WithMessage("Phải lớn hơn 0");
			RuleFor(x => x.TotalFee)
				.NotNull()
				.WithMessage("Không được để trống")
				.GreaterThanOrEqualTo(0)
				.WithMessage("Phải lớn hơn hoặc bằng 0");
			RuleFor(x => x.Discount)
				.NotNull()
				.WithMessage("Không được để trống")
				.GreaterThanOrEqualTo(0)
				.WithMessage("Phải lớn hơn hoặc bằng 0");
			When(x => x.PaymentType == PaymentTypeDto.MomoWallet, () =>
			{
				RuleFor(x => x.ReturnUrl)
				.NotNull()
				.WithMessage("Không được để trống");
			});
		}
	}
}
