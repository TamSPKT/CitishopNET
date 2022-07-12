namespace CitishopNET.Shared.Dtos.ApplicationUser
{
	public class RegisterUserDto
	{
		public string FullName { get; set; } = null!;
		public string Email { get; set; } = null!; // Email is aslo UserName
		public string Password { get; set; } = null!;
		public string ConfirmPassword { get; set; } = null!;
	}
}
