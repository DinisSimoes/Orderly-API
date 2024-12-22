using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Orderly.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserOnBD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("10f4d4bb-7382-435d-a5ee-ec7d348c3d75"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d77b18ad-36cb-44ec-a233-7c1b1f3d6dab"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("df030902-4636-4af5-8c2a-9bc52c973288"));

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Email", "Name", "Phone" },
                values: new object[] { new Guid("123e4567-e89b-12d3-a456-426614174000"), "teste@exemplo.com", "Teste", "+55123456789" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("1c55f143-dd46-4b38-bf15-c87b9d63172e"), "Product C", 30.00m },
                    { new Guid("43841b24-631a-4edb-8c90-94ce6acfc363"), "Product A", 10.00m },
                    { new Guid("7654d080-9b2f-4134-9c8e-8dfe96b1990c"), "Product B", 20.00m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("123e4567-e89b-12d3-a456-426614174000"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("1c55f143-dd46-4b38-bf15-c87b9d63172e"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("43841b24-631a-4edb-8c90-94ce6acfc363"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("7654d080-9b2f-4134-9c8e-8dfe96b1990c"));

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("10f4d4bb-7382-435d-a5ee-ec7d348c3d75"), "Product A", 10.00m },
                    { new Guid("d77b18ad-36cb-44ec-a233-7c1b1f3d6dab"), "Product B", 20.00m },
                    { new Guid("df030902-4636-4af5-8c2a-9bc52c973288"), "Product C", 30.00m }
                });
        }
    }
}
