using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodingWiki_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RenameMappingTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookAuthorMap_Authors_AuthorId",
                table: "BookAuthorMap");

            migrationBuilder.DropForeignKey(
                name: "FK_BookAuthorMap_Books_BookId",
                table: "BookAuthorMap");

            migrationBuilder.DropForeignKey(
                name: "FK_Fluent_BookAuthorMap_Fluent_Authors_AuthorId",
                table: "Fluent_BookAuthorMap");

            migrationBuilder.DropForeignKey(
                name: "FK_Fluent_BookAuthorMap_Fluent_Books_BookId",
                table: "Fluent_BookAuthorMap");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Fluent_BookAuthorMap",
                table: "Fluent_BookAuthorMap");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookAuthorMap",
                table: "BookAuthorMap");

            migrationBuilder.RenameTable(
                name: "Fluent_BookAuthorMap",
                newName: "Fluent_BookAuthorMaps");

            migrationBuilder.RenameTable(
                name: "BookAuthorMap",
                newName: "BookAuthorMaps");

            migrationBuilder.RenameIndex(
                name: "IX_Fluent_BookAuthorMap_AuthorId",
                table: "Fluent_BookAuthorMaps",
                newName: "IX_Fluent_BookAuthorMaps_AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_BookAuthorMap_AuthorId",
                table: "BookAuthorMaps",
                newName: "IX_BookAuthorMaps_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Fluent_BookAuthorMaps",
                table: "Fluent_BookAuthorMaps",
                columns: new[] { "BookId", "AuthorId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookAuthorMaps",
                table: "BookAuthorMaps",
                columns: new[] { "BookId", "AuthorId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BookAuthorMaps_Authors_AuthorId",
                table: "BookAuthorMaps",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "AuthorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookAuthorMaps_Books_BookId",
                table: "BookAuthorMaps",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Fluent_BookAuthorMaps_Fluent_Authors_AuthorId",
                table: "Fluent_BookAuthorMaps",
                column: "AuthorId",
                principalTable: "Fluent_Authors",
                principalColumn: "AuthorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Fluent_BookAuthorMaps_Fluent_Books_BookId",
                table: "Fluent_BookAuthorMaps",
                column: "BookId",
                principalTable: "Fluent_Books",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookAuthorMaps_Authors_AuthorId",
                table: "BookAuthorMaps");

            migrationBuilder.DropForeignKey(
                name: "FK_BookAuthorMaps_Books_BookId",
                table: "BookAuthorMaps");

            migrationBuilder.DropForeignKey(
                name: "FK_Fluent_BookAuthorMaps_Fluent_Authors_AuthorId",
                table: "Fluent_BookAuthorMaps");

            migrationBuilder.DropForeignKey(
                name: "FK_Fluent_BookAuthorMaps_Fluent_Books_BookId",
                table: "Fluent_BookAuthorMaps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Fluent_BookAuthorMaps",
                table: "Fluent_BookAuthorMaps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookAuthorMaps",
                table: "BookAuthorMaps");

            migrationBuilder.RenameTable(
                name: "Fluent_BookAuthorMaps",
                newName: "Fluent_BookAuthorMap");

            migrationBuilder.RenameTable(
                name: "BookAuthorMaps",
                newName: "BookAuthorMap");

            migrationBuilder.RenameIndex(
                name: "IX_Fluent_BookAuthorMaps_AuthorId",
                table: "Fluent_BookAuthorMap",
                newName: "IX_Fluent_BookAuthorMap_AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_BookAuthorMaps_AuthorId",
                table: "BookAuthorMap",
                newName: "IX_BookAuthorMap_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Fluent_BookAuthorMap",
                table: "Fluent_BookAuthorMap",
                columns: new[] { "BookId", "AuthorId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookAuthorMap",
                table: "BookAuthorMap",
                columns: new[] { "BookId", "AuthorId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BookAuthorMap_Authors_AuthorId",
                table: "BookAuthorMap",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "AuthorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookAuthorMap_Books_BookId",
                table: "BookAuthorMap",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Fluent_BookAuthorMap_Fluent_Authors_AuthorId",
                table: "Fluent_BookAuthorMap",
                column: "AuthorId",
                principalTable: "Fluent_Authors",
                principalColumn: "AuthorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Fluent_BookAuthorMap_Fluent_Books_BookId",
                table: "Fluent_BookAuthorMap",
                column: "BookId",
                principalTable: "Fluent_Books",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
