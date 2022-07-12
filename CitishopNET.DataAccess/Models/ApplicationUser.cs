using Microsoft.AspNetCore.Identity;

namespace CitishopNET.DataAccess.Models
{
	public class ApplicationUser : IdentityUser
	{
		[PersonalData]
		public string FullName { get; set; } = null!;

		[PersonalData]
		public DateTime? DOB { get; set; }

		public bool IsAdmin { get; set; }

		// Navigation property
		public virtual List<UserDeliveryAddress> Addresses { get; set; } = null!;
		public virtual List<Invoice> Invoices { get; set; } = null!;
	}
}
