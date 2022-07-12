using CitishopNET.Shared.Dtos.ApplicationUser;
using FluentValidation;

namespace CitishopNET.Validators.ApplicationUser
{
	/// <summary>
	/// Validator for EditUserDto.
	/// <para></para>
	/// Don't need to create validators in controllers as 
	/// RegisterValidatorsFromAssembly(...) in Program.cs 
	/// registers all validators derived from AbstractValidator within this assembly.
	/// </summary>
	public class EditUserValidator : AbstractValidator<EditUserDto>
	{
		public EditUserValidator()
		{
			RuleFor(x => x.FullName)
				.NotEmpty()
				.WithMessage("Không được để trống");
			RuleFor(x => x.PhoneNumber)
				.NotEmpty()
				.WithMessage("Không được để trống")
				.Matches("^(84|0[3|5|7|8|9])+([0-9]{8})$")
				.WithMessage("Số điện thoại không hợp lệ");
			RuleFor(x => x.DateOfBirth)
				.NotEmpty()
				.WithMessage("Không được để trống");
		}
	}
}
