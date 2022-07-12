using CitishopNET.Shared.Dtos.ApplicationUser;
using FluentValidation;

namespace CitishopNET.Validators.ApplicationUser
{
	/// <summary>
	/// Validator for RegisterUserDto.
	/// <para></para>
	/// Don't need to create validators in controllers as 
	/// RegisterValidatorsFromAssembly(...) in Program.cs 
	/// registers all validators derived from AbstractValidator within this assembly.
	/// </summary>
	public class RegisterUserValidator : AbstractValidator<RegisterUserDto>
	{
		public RegisterUserValidator()
		{
			RuleFor(x => x.FullName)
				.NotEmpty()
				.WithMessage("Không được để trống");
			RuleFor(x => x.Email)
				.EmailAddress()
				.WithMessage("Email không hợp lệ");
			RuleFor(x => x.Password)
				.NotEmpty()
				.WithMessage("Không được để trống");
			When(x => !string.IsNullOrWhiteSpace(x.Password), () =>
			{
				RuleFor(x => x.ConfirmPassword)
				.Equal(x => x.Password)
				.WithMessage("Không trùng khớp với mật khẩu");
			});
		}
	}
}
