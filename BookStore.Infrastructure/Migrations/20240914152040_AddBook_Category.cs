using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBook_Category : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    ReleaseYear = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BookCategoryName = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Book_Category",
                columns: table => new
                {
                    BookId = table.Column<Guid>(type: "uuid", nullable: false),
                    BookCategoryId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book_Category", x => new { x.BookId, x.BookCategoryId });
                    table.ForeignKey(
                        name: "FK_Book_Category_BookCategory_BookCategoryId",
                        column: x => x.BookCategoryId,
                        principalTable: "BookCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Book_Category_Book_BookId",
                        column: x => x.BookId,
                        principalTable: "Book",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Book_Category_BookCategoryId",
                table: "Book_Category",
                column: "BookCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Book_Category");

            migrationBuilder.DropTable(
                name: "BookCategory");

            migrationBuilder.DropTable(
                name: "Book");
        }
    }
}
