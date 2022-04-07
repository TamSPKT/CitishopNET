using CitishopNET.Shared.Dtos.Product;
using FluentValidation;

namespace CitishopNET.Validators.Product
{
	/// <summary>
	/// Validator for EditProductDto.
	/// <para></para>
	/// Don't need to create validators in controllers as 
	/// RegisterValidatorsFromAssembly(...) in Program.cs 
	/// registers all validators derived from AbstractValidator within this assembly.
	/// </summary>
	public class EditProductValidator: AbstractValidator<EditProductDto>
	{
		public EditProductValidator()
		{
			RuleFor(x => x.Name)
				.NotEmpty()
				.WithMessage("Không được để trống");
			RuleFor(x => x.Price)
				.GreaterThan(0)
				.WithMessage(x => $"Phải lớn hơn 0");
			When(x => x.DiscountPrice.HasValue, () =>
			{
				RuleFor(x => x.DiscountPrice)
				.GreaterThan(0)
				.WithMessage(x => $"Phải lớn hơn 0");
				When(x => x.Price > 0, () =>
				{
					RuleFor(x => x.DiscountPrice)
					.LessThan(y => y.Price)
					.WithMessage(x => $"Phải nhỏ hơn {x.Price}");
				});
			});
			RuleFor(x => x.Quantity)
				.GreaterThan(0)
				.WithMessage(x => $"Phải lớn hơn 0");
			RuleFor(x => x.Description)
				.MaximumLength(500)
				.WithMessage("Quá giới hạn 500 ký tự");
			RuleFor(x => x.ImageUrl)
				.NotEmpty()
				.WithMessage("Không được để trống");
		}
	}
}
