namespace CitishopNET.Shared.Dtos.ApplicationUser
{
	public class ResetPasswordDto
	{
		public string Email { get; set; }
		public string Password { get; set; }
		public string ConfirmPassword { get; set; }
	}
}
