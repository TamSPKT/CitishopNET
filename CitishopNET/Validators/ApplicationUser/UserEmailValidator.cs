using CitishopNET.Shared.Dtos.ApplicationUser;
using FluentValidation;

namespace CitishopNET.Validators.ApplicationUser
{
	/// <summary>
	/// Validator for UserEmailDto.
	/// <para></para>
	/// Don't need to create validators in controllers as 
	/// RegisterValidatorsFromAssembly(...) in Program.cs 
	/// registers all validators derived from AbstractValidator within this assembly.
	/// </summary>
	public class UserEmailValidator : AbstractValidator<UserEmailDto>
	{
		public UserEmailValidator()
		{
			RuleFor(x => x.Email)
				.EmailAddress()
				.WithMessage("Email không hợp lệ");
		}
	}
}
