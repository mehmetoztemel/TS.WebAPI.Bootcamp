using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ECommerce.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "CreatedDate", "Email", "FirstName", "LastName", "Password", "UpdatedDate" },
                values: new object[] { new Guid("c80e56c7-0a0a-40a8-b30a-763e5e8da53b"), new DateTime(2024, 5, 8, 17, 9, 30, 860, DateTimeKind.Local).AddTicks(5352), "admin@admin.com", "Admin", "Admin", "1234", null });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedDate", "Description", "Name", "Price", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("cedc9f23-3d24-40b0-834a-00d3e992df26"), new DateTime(2024, 5, 8, 17, 9, 30, 860, DateTimeKind.Local).AddTicks(7261), "Black Ballpoint Pen", "Black Ballpoint Pen", 5m, null },
                    { new Guid("e8cdf575-4e49-4ed7-a3ea-3c7b4f11cfbd"), new DateTime(2024, 5, 8, 17, 9, 30, 860, DateTimeKind.Local).AddTicks(7288), "Red Ballpoint Pen", "Red Ballpoint Pen", 7m, null },
                    { new Guid("ee7d4f83-cb5f-48d9-bbe1-b8cfd4d8d929"), new DateTime(2024, 5, 8, 17, 9, 30, 860, DateTimeKind.Local).AddTicks(7284), "Blue Ballpoint Pen", "Blue Ballpoint Pen", 6m, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
