using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPricesToOrderAndOrderItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CreateOrders");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "OrderItems",
                newName: "Price");

            migrationBuilder.AddColumn<int>(
                name: "TotalPrice",
                table: "Orders",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "OrderItems",
                newName: "price");

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
    }
}
