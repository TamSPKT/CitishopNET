﻿namespace CitishopNET.Shared.Dtos.ApplicationUser
{
	public class RegisterUserDto
	{
		public string UserName { get; set; }
		public string Password { get; set; }
		public string ConfirmPassword { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public string Address { get; set; }
		public DateTime DateOfBirth { get; set; }
		public string? Gender { get; set; }
	}
}
