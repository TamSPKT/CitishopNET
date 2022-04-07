using Microsoft.AspNetCore.Identity;

namespace CitishopNET.DataAccess.Models
{
	public class ApplicationUser : IdentityUser
	{
		[PersonalData]
		public string Address { get; set; }

		[PersonalData]
		public DateTime DOB { get; set; }

		[PersonalData]
		public string? Gender { get; set; }

		// Navigation property
		public virtual List<Invoice> Invoices { get; set; }
	}
}
