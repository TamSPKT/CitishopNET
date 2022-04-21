using CitishopNET.Shared.Dtos.ApplicationUser;
using FluentValidation;

namespace CitishopNET.Validators.ApplicationUser
{
	/// <summary>
	/// Validator for ChangePasswordDto.
	/// <para></para>
	/// Don't need to create validators in controllers as 
	/// RegisterValidatorsFromAssembly(...) in Program.cs 
	/// registers all validators derived from AbstractValidator within this assembly.
	/// </summary>
	public class ChangePasswordValidator : AbstractValidator<ChangePasswordDto>
	{
		public ChangePasswordValidator()
		{
			RuleFor(x => x.OldPassword)
				.NotEmpty()
				.WithMessage("Không được để trống");
			When(x => !string.IsNullOrWhiteSpace(x.OldPassword), () =>
			{
				RuleFor(x => x.NewPassword)
				.NotEmpty()
				.WithMessage("Không được để trống")
				.NotEqual(x => x.OldPassword)
				.WithMessage("Mật khẩu mới trùng khớp mật khẩu cũ");
			});
			When(x => !string.IsNullOrWhiteSpace(x.NewPassword), () =>
			{
				RuleFor(x => x.ConfirmPassword)
				.Equal(x => x.NewPassword)
				.WithMessage("Không trùng khớp với mật khẩu mới");
			});
		}
	}
}
