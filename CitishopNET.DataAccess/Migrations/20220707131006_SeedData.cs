using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CitishopNET.DataAccess.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0bf8df34-1177-4935-85f4-ccfa035973b9"), "Chăm sóc tóc" },
                    { new Guid("512df5ef-f164-47ab-a52a-f0a3650634df"), "Chăm sóc da" },
                    { new Guid("559517fa-27a0-46f4-9d01-3baa47c3db20"), "Thực phẩm chức năng" },
                    { new Guid("6ce4f5b2-244c-4c4e-9da9-63ac829c4d66"), "Dành cho nam" },
                    { new Guid("a299c3cb-d65a-4405-a53b-74f29d688dfa"), "Chăm sóc cơ thể" },
                    { new Guid("acc874a2-7492-4bab-88ac-f017ade9158e"), "Vệ sinh cá nhân" },
                    { new Guid("ba4f7300-66af-45a6-b0d4-ee3cf5a7eae8"), "Trang điểm" },
                    { new Guid("cf07b43d-1527-49bd-a3b8-8a210ceb50fb"), "Nước hoa" },
                    { new Guid("e4cdba4a-a804-4026-a64c-d57570437187"), "Phụ kiện" },
                    { new Guid("e8449ff4-cfbf-4447-8054-e29545000372"), "Dành cho em bé" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "DiscountPrice", "ImageUrl", "Name", "Price", "Quantity" },
                values: new object[,]
                {
                    { new Guid("00275660-559f-40cc-a882-96d6a5e99c76"), new Guid("512df5ef-f164-47ab-a52a-f0a3650634df"), "Sữa Chống Nắng Anessa Dưỡng Da Kiềm Dầu 60ml", null, "/assets/images/products/kcnanness.jpg", "Sữa Chống Nắng Anessa Dưỡng Da Kiềm Dầu 60ml", 250000m, 100 },
                    { new Guid("6ba8c452-4c2b-4121-9b2c-fbe3d6d3d65b"), new Guid("512df5ef-f164-47ab-a52a-f0a3650634df"), "Toner De Klairs", null, "/assets/images/products/tonerKlairs.jpg", "Toner De Klairs", 250000m, 100 },
                    { new Guid("9c965823-afd5-4559-8f95-aef7cdc95d23"), new Guid("512df5ef-f164-47ab-a52a-f0a3650634df"), "Tẩy trang Loreal", null, "/assets/images/products/taytrangloreal.jpg", "Tẩy trang Loreal", 250000m, 100 },
                    { new Guid("ae79ae19-8664-464b-880f-909a78fe3bf2"), new Guid("ba4f7300-66af-45a6-b0d4-ee3cf5a7eae8"), "Son Kem Lì Nhẹ Môi Cao Cấp LOréal Paris 119 I Dream 7ml", null, "/assets/images/products/sonloreal.jpg", "Son Kem Lì Nhẹ Môi Cao Cấp LOréal Paris 119 I Dream 7ml", 250000m, 100 },
                    { new Guid("af732951-1b63-4ef2-a87e-e6b345977b53"), new Guid("ba4f7300-66af-45a6-b0d4-ee3cf5a7eae8"), "Son 3CE", null, "/assets/images/products/son3ce.jpg", "Son 3CE", 250000m, 100 },
                    { new Guid("bb5dbb97-a510-42df-a286-1531a23ebace"), new Guid("512df5ef-f164-47ab-a52a-f0a3650634df"), "Sữa Rửa Mặt Cetaphil Dịu Nhẹ Không Xà Phòng 500ml", null, "/assets/images/products/srmcetaphil.jpg", "Sữa Rửa Mặt Cetaphil Dịu Nhẹ Không Xà Phòng 500ml", 250000m, 100 },
                    { new Guid("ecee190c-620c-4145-abc5-06049f6016d6"), new Guid("512df5ef-f164-47ab-a52a-f0a3650634df"), "Nước Tẩy Trang La Roche-Posay Dành Cho Da Nhạy Cảm 400ml", null, "/assets/images/products/taytranglarocheposay.jpg", "Nước Tẩy Trang La Roche-Posay Dành Cho Da Nhạy Cảm 400ml", 250000m, 100 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("0bf8df34-1177-4935-85f4-ccfa035973b9"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("559517fa-27a0-46f4-9d01-3baa47c3db20"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("6ce4f5b2-244c-4c4e-9da9-63ac829c4d66"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a299c3cb-d65a-4405-a53b-74f29d688dfa"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("acc874a2-7492-4bab-88ac-f017ade9158e"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("cf07b43d-1527-49bd-a3b8-8a210ceb50fb"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("e4cdba4a-a804-4026-a64c-d57570437187"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("e8449ff4-cfbf-4447-8054-e29545000372"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00275660-559f-40cc-a882-96d6a5e99c76"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("6ba8c452-4c2b-4121-9b2c-fbe3d6d3d65b"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("9c965823-afd5-4559-8f95-aef7cdc95d23"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("ae79ae19-8664-464b-880f-909a78fe3bf2"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("af732951-1b63-4ef2-a87e-e6b345977b53"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("bb5dbb97-a510-42df-a286-1531a23ebace"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("ecee190c-620c-4145-abc5-06049f6016d6"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("512df5ef-f164-47ab-a52a-f0a3650634df"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("ba4f7300-66af-45a6-b0d4-ee3cf5a7eae8"));
        }
    }
}
