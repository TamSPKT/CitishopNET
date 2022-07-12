using CitishopNET.Shared.Dtos.UserDeliveryAddress;
using FluentValidation;

namespace CitishopNET.Validators.UserDeliveryAddress
{
	/// <summary>
	/// Validator for CreateUserDeliveryAddressDto.
	/// <para></para>
	/// Don't need to create validators in controllers as 
	/// RegisterValidatorsFromAssembly(...) in Program.cs 
	/// registers all validators derived from AbstractValidator within this assembly.
	/// </summary>
	public class CreateAddressValidator : AbstractValidator<CreateUserDeliveryAddressDto>
	{
		public CreateAddressValidator()
		{
			RuleFor(x => x.UserId)
				.NotEmpty()
				.WithMessage("Không được để trống");
			RuleFor(x => x.ReceiverName)
				.NotEmpty()
				.WithMessage("Không được để trống");
			RuleFor(x => x.DeliveryAddress)
				.NotEmpty()
				.WithMessage("Không được để trống");
			RuleFor(x => x.PhoneNumber)
				.NotEmpty()
				.WithMessage("Không được để trống")
				.Matches("^(84|0[3|5|7|8|9])+([0-9]{8})$")
				.WithMessage("Số điện thoại không hợp lệ");
		}
	}
}
