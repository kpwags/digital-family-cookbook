using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalFamilyCookbook.Migrations
{
    public partial class UpdatedNotesStructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notes",
                schema: "recipe",
                table: "Recipe");

            migrationBuilder.AlterColumn<string>(
                name: "InvitationCode",
                schema: "application",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "50ece01e-7e35-4191-8159-ec0d322aeef9",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "cf87ae4a-36a3-4d80-948d-e877389a2228");

            migrationBuilder.CreateTable(
                name: "Note",
                schema: "application",
                columns: table => new
                {
                    NoteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "ff2ce021-1b23-4b22-b520-4b409dffae03"),
                    NoteText = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Application_Note_NoteId", x => x.NoteId);
                });

            migrationBuilder.CreateTable(
                name: "RecipeNote",
                schema: "recipe",
                columns: table => new
                {
                    RecipeNoteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "f08052b7-ebd3-4400-bdd1-207274066a36"),
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    NoteId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipe_RecipeNote_RecipeNoteId", x => x.RecipeNoteId);
                    table.ForeignKey(
                        name: "FK_RecipeNote_Note_NoteId",
                        column: x => x.NoteId,
                        principalSchema: "application",
                        principalTable: "Note",
                        principalColumn: "NoteId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecipeNote_Recipe_RecipeId",
                        column: x => x.RecipeId,
                        principalSchema: "recipe",
                        principalTable: "Recipe",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecipeNote_NoteId",
                schema: "recipe",
                table: "RecipeNote",
                column: "NoteId");

            migrationBuilder.CreateIndex(
                name: "UQ_Recipe_RecipeNote_RecipeId_NoteId",
                schema: "recipe",
                table: "RecipeNote",
                columns: new[] { "RecipeId", "NoteId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecipeNote",
                schema: "recipe");

            migrationBuilder.DropTable(
                name: "Note",
                schema: "application");

            migrationBuilder.AlterColumn<string>(
                name: "InvitationCode",
                schema: "application",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "cf87ae4a-36a3-4d80-948d-e877389a2228",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "50ece01e-7e35-4191-8159-ec0d322aeef9");

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                schema: "recipe",
                table: "Recipe",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);
        }
    }
}
