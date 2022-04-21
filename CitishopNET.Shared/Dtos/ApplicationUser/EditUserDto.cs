namespace CitishopNET.Shared.Dtos.ApplicationUser
{
	public class EditUserDto
	{
		public string PhoneNumber { get; set; }
		public string Address { get; set; }
		public DateTime DateOfBirth { get; set; }
		public string? Gender { get; set; }
	}
}
