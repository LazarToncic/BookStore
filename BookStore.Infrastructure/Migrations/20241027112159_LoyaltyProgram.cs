using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LoyaltyProgram : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "4DAF65CB-CC0E-4C81-9183-20097EA81F5A");

            migrationBuilder.AddColumn<bool>(
                name: "DiscountActive",
                table: "Orders",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountAmount",
                table: "Orders",
                type: "numeric",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LoyaltyProgram",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LoyaltyPoints = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    DiscountPercentage = table.Column<decimal>(type: "numeric", nullable: false),
                    EnrollmentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoyaltyProgram", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoyaltyProgram_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoyaltyProgram_UserId",
                table: "LoyaltyProgram",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoyaltyProgram");

            migrationBuilder.DropColumn(
                name: "DiscountActive",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DiscountAmount",
                table: "Orders");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "4DAF65CB-CC0E-4C81-9183-20097EA81F5A", 0, "c188a435-cfc8-45fd-836c-9a18bb9de405", "ltoncic@gmail.com", true, "Lazar", "Toncic", false, null, "LTONCIC@gmail.com", "LTONCIC@GMAIL.COM", null, null, true, "00000000-0000-0000-0000-000000000000", false, "ltoncic@gmail.com" });
        }
    }
}
