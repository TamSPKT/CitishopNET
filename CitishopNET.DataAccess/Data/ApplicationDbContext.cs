using CitishopNET.DataAccess.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CitishopNET.DataAccess.Data
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<ApplicationUser>(entity =>
			{
				entity.ToTable(name: "Users");
			});
			builder.Entity<Product>(entity =>
			{
				entity.ToTable(name: "Products");
			});
			builder.Entity<Invoice>(entity =>
			{
				entity.ToTable(name: "Invoices");
			});
			builder.Entity<InvoiceProduct>(entity =>
			{
				entity.ToTable(name: "InvoiceProducts");
				entity.HasKey(e => new { e.ProductId, e.InvoiceId });
			});
		}
	}
}
