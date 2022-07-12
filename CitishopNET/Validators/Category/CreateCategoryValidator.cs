using CitishopNET.Shared.Dtos.Category;
using FluentValidation;

namespace CitishopNET.Validators.Category
{
	/// <summary>
	/// Validator for CreateCategoryDto.
	/// <para></para>
	/// Don't need to create validators in controllers as 
	/// RegisterValidatorsFromAssembly(...) in Program.cs 
	/// registers all validators derived from AbstractValidator within this assembly.
	/// </summary>
	public class CreateCategoryValidator : AbstractValidator<CreateCategoryDto>
	{
		public CreateCategoryValidator()
		{
			RuleFor(x => x.Name)
				.NotEmpty()
				.WithMessage("Không được để trống");
		}
	}
}
