using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookStoreAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author", x => x.AuthorId);
                });

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SubTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_Book_Author_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Author",
                        principalColumn: "AuthorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Author",
                columns: new[] { "AuthorId", "Name" },
                values: new object[,]
                {
                    { 1, "Charles Dickens" },
                    { 2, "Paulo Coelho" },
                    { 3, "J. K. Rowling" },
                    { 4, "Agatha Christie" },
                    { 5, "William Shakespeare" },
                    { 6, "Stephen King" },
                    { 7, "Jane Austen" },
                    { 8, "Leo Tolstoy" }
                });

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "BookId", "AuthorId", "SubTitle", "Title" },
                values: new object[,]
                {
                    { 1, 1, null, "A Tale of Two Cities" },
                    { 2, 1, null, "Dombey and Son" },
                    { 3, 1, null, "David Copperfield" },
                    { 4, 1, null, "Martin Chuzzlewit" },
                    { 5, 2, null, "The Alchemist" },
                    { 6, 2, null, "Brida" },
                    { 7, 2, null, "Aleph" },
                    { 8, 2, null, "Amor" },
                    { 9, 2, null, "Brida" },
                    { 10, 2, null, "Brida" },
                    { 11, 2, null, "Brida" },
                    { 12, 2, null, "Brida" },
                    { 13, 3, null, "Harry Potter and the Sorcerer's Stone" },
                    { 14, 3, null, "Harry Potter and the Chamber of Secrets" },
                    { 15, 3, null, "Harry Potter and the Prisoner of Azkaban" },
                    { 16, 3, null, "Harry Potter and the Goblet of Fire" },
                    { 17, 3, null, "Harry Potter and the Order of the Phoenix" },
                    { 18, 3, null, "Harry Potter and the Half-Blood Prince" },
                    { 19, 3, null, "Harry Potter and the Deathly Hallows" },
                    { 20, 3, null, "Harry Potter and the Cursed Child" },
                    { 21, 4, null, "And Then There Were None" },
                    { 22, 4, null, "The Murder of Roger Ackroyd" },
                    { 23, 4, null, "The Hollow" },
                    { 24, 5, null, "Hamlet" },
                    { 25, 5, null, "Romeo and Juliet" },
                    { 26, 6, null, "The Shining" },
                    { 27, 7, null, "Pride and Prejudice" },
                    { 28, 8, null, "War and Peace" },
                    { 29, 8, null, "Anna Karenina" },
                    { 30, 8, null, "Resurrection" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Book_AuthorId",
                table: "Book",
                column: "AuthorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "Author");
        }
    }
}
