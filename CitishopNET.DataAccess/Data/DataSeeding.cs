using CitishopNET.DataAccess.Models;

namespace CitishopNET.DataAccess.Data
{
	public static class DataSeeding
	{
		public static IEnumerable<Category> SeedCategories()
		{
			return new List<Category>
			{
				new Category
				{
					Id = new Guid("ba4f7300-66af-45a6-b0d4-ee3cf5a7eae8"),
					Name = "Trang điểm",
				},
				new Category
				{
					Id = new Guid("512df5ef-f164-47ab-a52a-f0a3650634df"),
					Name = "Chăm sóc da",
				},
				new Category
				{
					Id = new Guid("a299c3cb-d65a-4405-a53b-74f29d688dfa"),
					Name = "Chăm sóc cơ thể",
				},
				new Category
				{
					Id = new Guid("0bf8df34-1177-4935-85f4-ccfa035973b9"),
					Name = "Chăm sóc tóc",
				},
				new Category
				{
					Id = new Guid("cf07b43d-1527-49bd-a3b8-8a210ceb50fb"),
					Name = "Nước hoa",
				},
				new Category
				{
					Id = new Guid("559517fa-27a0-46f4-9d01-3baa47c3db20"),
					Name = "Thực phẩm chức năng",
				},
				new Category
				{
					Id = new Guid("e4cdba4a-a804-4026-a64c-d57570437187"),
					Name = "Phụ kiện",
				},
				new Category
				{
					Id = new Guid("6ce4f5b2-244c-4c4e-9da9-63ac829c4d66"),
					Name = "Dành cho nam",
				},
				new Category
				{
					Id = new Guid("e8449ff4-cfbf-4447-8054-e29545000372"),
					Name = "Dành cho em bé",
				},
				new Category
				{
					Id = new Guid("acc874a2-7492-4bab-88ac-f017ade9158e"),
					Name = "Vệ sinh cá nhân",
				},
			};
		}

		public static IEnumerable<Product> SeedProducts()
		{
			return new List<Product>
			{
				new Product
				{
					Id = new Guid("ae79ae19-8664-464b-880f-909a78fe3bf2"),
					Name = "Son Kem Lì Nhẹ Môi Cao Cấp LOréal Paris 119 I Dream 7ml",
					ImageUrl = "/assets/images/products/sonloreal.jpg",
					Price = 250_000M,
					CategoryId = new Guid("ba4f7300-66af-45a6-b0d4-ee3cf5a7eae8"), // Trang điểm
					Quantity = 100,
					Description = "Son Kem Lì Nhẹ Môi Cao Cấp LOréal Paris 119 I Dream 7ml",
				},
				new Product
				{
					Id = new Guid("9c965823-afd5-4559-8f95-aef7cdc95d23"),
					Name = "Tẩy trang Loreal",
					ImageUrl = "/assets/images/products/taytrangloreal.jpg",
					Price = 250_000M,
					CategoryId = new Guid("512df5ef-f164-47ab-a52a-f0a3650634df"), // Chăm sóc da
					Quantity = 100,
					Description = "Tẩy trang Loreal",
				},
				new Product
				{
					Id = new Guid("6ba8c452-4c2b-4121-9b2c-fbe3d6d3d65b"),
					Name = "Toner De Klairs",
					ImageUrl = "/assets/images/products/tonerKlairs.jpg",
					Price = 250_000M,
					CategoryId = new Guid("512df5ef-f164-47ab-a52a-f0a3650634df"), // Chăm sóc da
					Quantity = 100,
					Description = "Toner De Klairs",
				},
				new Product
				{
					Id = new Guid("af732951-1b63-4ef2-a87e-e6b345977b53"),
					Name = "Son 3CE",
					ImageUrl = "/assets/images/products/son3ce.jpg",
					Price = 250_000M,
					CategoryId = new Guid("ba4f7300-66af-45a6-b0d4-ee3cf5a7eae8"), // Trang điểm
					Quantity = 100,
					Description = "Son 3CE",
				},
				new Product
				{
					Id = new Guid("ecee190c-620c-4145-abc5-06049f6016d6"),
					Name = "Nước Tẩy Trang La Roche-Posay Dành Cho Da Nhạy Cảm 400ml",
					ImageUrl = "/assets/images/products/taytranglarocheposay.jpg",
					Price = 250_000M,
					CategoryId = new Guid("512df5ef-f164-47ab-a52a-f0a3650634df"), // Chăm sóc da
					Quantity = 100,
					Description = "Nước Tẩy Trang La Roche-Posay Dành Cho Da Nhạy Cảm 400ml",
				},
				new Product
				{
					Id = new Guid("bb5dbb97-a510-42df-a286-1531a23ebace"),
					Name = "Sữa Rửa Mặt Cetaphil Dịu Nhẹ Không Xà Phòng 500ml",
					ImageUrl = "/assets/images/products/srmcetaphil.jpg",
					Price = 250_000M,
					CategoryId = new Guid("512df5ef-f164-47ab-a52a-f0a3650634df"), // Chăm sóc da
					Quantity = 100,
					Description = "Sữa Rửa Mặt Cetaphil Dịu Nhẹ Không Xà Phòng 500ml",
				},
				new Product
				{
					Id = new Guid("00275660-559f-40cc-a882-96d6a5e99c76"),
					Name = "Sữa Chống Nắng Anessa Dưỡng Da Kiềm Dầu 60ml",
					ImageUrl = "/assets/images/products/kcnanness.jpg",
					Price = 250_000M,
					CategoryId = new Guid("512df5ef-f164-47ab-a52a-f0a3650634df"), // Chăm sóc da
					Quantity = 100,
					Description = "Sữa Chống Nắng Anessa Dưỡng Da Kiềm Dầu 60ml",
				},
			};
		}

	}
}
