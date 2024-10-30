using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Order_OneFreeBookDiscount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "OneFreeBookDiscount",
                table: "Orders",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OneFreeBookDiscount",
                table: "Orders");
        }
    }
}
