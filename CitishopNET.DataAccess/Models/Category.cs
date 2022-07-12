namespace CitishopNET.DataAccess.Models
{
	public class Category
	{
		public Guid Id { get; set; }
		public string Name { get; set; } = null!;

		public virtual List<Product> Products { get; set; } = null!;
	}
}
