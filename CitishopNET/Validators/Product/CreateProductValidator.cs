using CitishopNET.Shared.Dtos.Product;
using FluentValidation;

namespace CitishopNET.Validators.Product
{
	/// <summary>
	/// Validator for CreateProductDto.
	/// <para></para>
	/// Don't need to create validators in controllers as 
	/// RegisterValidatorsFromAssembly(...) in Program.cs 
	/// registers all validators derived from AbstractValidator within this assembly.
	/// </summary>
	public class CreateProductValidator : AbstractValidator<CreateProductDto>
	{
		public CreateProductValidator()
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
				.MaximumLength(5000)
				.WithMessage("Quá giới hạn 5000 ký tự");
			RuleFor(x => x.ImageUrl)
				.NotEmpty()
				.WithMessage("Không được để trống");
			RuleFor(x => x.CategoryId)
				.NotEmpty()
				.WithMessage("Không được để trống");
		}
	}
}
