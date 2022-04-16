using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalFamilyCookbook.Migrations
{
    public partial class CorrectMeatTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meat_Recipe_RecipeId",
                schema: "recipe",
                table: "Meat");

            migrationBuilder.DropIndex(
                name: "IX_Meat_RecipeId",
                schema: "recipe",
                table: "Meat");

            migrationBuilder.DropColumn(
                name: "RecipeId",
                schema: "recipe",
                table: "Meat");

            migrationBuilder.AlterColumn<string>(
                name: "InvitationCode",
                schema: "application",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "cf87ae4a-36a3-4d80-948d-e877389a2228",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "62fa4988-9a70-4c81-b51e-f67cda34691e");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "InvitationCode",
                schema: "application",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "62fa4988-9a70-4c81-b51e-f67cda34691e",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "cf87ae4a-36a3-4d80-948d-e877389a2228");

            migrationBuilder.AddColumn<int>(
                name: "RecipeId",
                schema: "recipe",
                table: "Meat",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Meat_RecipeId",
                schema: "recipe",
                table: "Meat",
                column: "RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Meat_Recipe_RecipeId",
                schema: "recipe",
                table: "Meat",
                column: "RecipeId",
                principalSchema: "recipe",
                principalTable: "Recipe",
                principalColumn: "RecipeId");
        }
    }
}
