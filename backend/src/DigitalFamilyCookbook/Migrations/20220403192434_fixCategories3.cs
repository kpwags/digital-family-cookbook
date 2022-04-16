using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalFamilyCookbook.Migrations
{
    public partial class fixCategories3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Recipe_RecipeDtoRecipeId",
                schema: "recipe",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Category_RecipeDtoRecipeId",
                schema: "recipe",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "RecipeDtoRecipeId",
                schema: "recipe",
                table: "Category");

            migrationBuilder.AlterColumn<string>(
                name: "InvitationCode",
                schema: "application",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "848b550d-74a3-4d3a-8ccf-35139d087f5c",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "23ab8c4e-4a2c-4e94-b448-8549371086cc");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "InvitationCode",
                schema: "application",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "23ab8c4e-4a2c-4e94-b448-8549371086cc",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "848b550d-74a3-4d3a-8ccf-35139d087f5c");

            migrationBuilder.AddColumn<int>(
                name: "RecipeDtoRecipeId",
                schema: "recipe",
                table: "Category",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Category_RecipeDtoRecipeId",
                schema: "recipe",
                table: "Category",
                column: "RecipeDtoRecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Recipe_RecipeDtoRecipeId",
                schema: "recipe",
                table: "Category",
                column: "RecipeDtoRecipeId",
                principalSchema: "recipe",
                principalTable: "Recipe",
                principalColumn: "RecipeId");
        }
    }
}
