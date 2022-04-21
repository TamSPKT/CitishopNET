using CitishopNET.Shared.Dtos.ApplicationUser;
using FluentValidation;

namespace CitishopNET.Validators.ApplicationUser
{
	/// <summary>
	/// Validator for ResetPasswordDto.
	/// <para></para>
	/// Don't need to create validators in controllers as 
	/// RegisterValidatorsFromAssembly(...) in Program.cs 
	/// registers all validators derived from AbstractValidator within this assembly.
	/// </summary>
	public class ResetPasswordValidator : AbstractValidator<ResetPasswordDto>
	{
		public ResetPasswordValidator()
		{
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
