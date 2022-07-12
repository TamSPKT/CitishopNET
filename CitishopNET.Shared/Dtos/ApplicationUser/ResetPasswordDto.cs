namespace CitishopNET.Shared.Dtos.ApplicationUser
{
	public class ResetPasswordDto
	{
		public string Email { get; set; } = null!;
		public string Password { get; set; } = null!;
		public string ConfirmPassword { get; set; } = null!;
	}
}
