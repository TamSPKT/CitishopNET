namespace CitishopNET.Shared.Dtos.ApplicationUser
{
	public class UserDto
	{
		public string Id { get; set; } = null!;
		public string FullName { get; set; } = null!;
		public string Email { get; set; } = null!;
		public bool EmailConfirmed { get; set; }
		public string PhoneNumber { get; set; } = null!;
		public DateTime? DateOfBirth { get; set; }
		public bool IsAdmin { get; set; }
	}
}
