namespace CitishopNET.Shared.Dtos.ApplicationUser
{
	public class EditUserDto
	{
		public string FullName { get; set; } = null!;
		public string PhoneNumber { get; set; } = null!;
		public DateTime DateOfBirth { get; set; }
	}
}
