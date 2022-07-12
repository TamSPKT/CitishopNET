using CitishopNET.Shared.Dtos.ApplicationUser;
using FluentValidation;

namespace CitishopNET.Validators.ApplicationUser
{
	/// <summary>
	/// Validator for EditUserRoleDto.
	/// <para></para>
	/// Don't need to create validators in controllers as 
	/// RegisterValidatorsFromAssembly(...) in Program.cs 
	/// registers all validators derived from AbstractValidator within this assembly.
	/// </summary>
	public class EditUserRoleValidator : AbstractValidator<EditUserRoleDto>
	{
		public EditUserRoleValidator()
		{
			RuleFor(x => x.IsAdmin)
				.NotNull()
				.WithMessage("Không được để trống");
		}
	}
}
