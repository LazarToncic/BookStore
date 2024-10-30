using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPricesToBooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Book",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CreateOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreateOrders", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CreateOrders");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Book");
        }
    }
}
