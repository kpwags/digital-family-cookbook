using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalFamilyCookbook.Migrations
{
    public partial class fixCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Recipe_RecipeId",
                schema: "recipe",
                table: "Category");

            migrationBuilder.RenameColumn(
                name: "RecipeId",
                schema: "recipe",
                table: "Category",
                newName: "RecipeDtoRecipeId");

            migrationBuilder.RenameIndex(
                name: "IX_Category_RecipeId",
                schema: "recipe",
                table: "Category",
                newName: "IX_Category_RecipeDtoRecipeId");

            migrationBuilder.AlterColumn<string>(
                name: "InvitationCode",
                schema: "application",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "eeb79ed3-d927-420e-9c55-cef8e63f496e",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "9d4780c1-21d4-4ea8-b053-e53cad63e8e8");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Recipe_RecipeDtoRecipeId",
                schema: "recipe",
                table: "Category",
                column: "RecipeDtoRecipeId",
                principalSchema: "recipe",
                principalTable: "Recipe",
                principalColumn: "RecipeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Recipe_RecipeDtoRecipeId",
                schema: "recipe",
                table: "Category");

            migrationBuilder.RenameColumn(
                name: "RecipeDtoRecipeId",
                schema: "recipe",
                table: "Category",
                newName: "RecipeId");

            migrationBuilder.RenameIndex(
                name: "IX_Category_RecipeDtoRecipeId",
                schema: "recipe",
                table: "Category",
                newName: "IX_Category_RecipeId");

            migrationBuilder.AlterColumn<string>(
                name: "InvitationCode",
                schema: "application",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "9d4780c1-21d4-4ea8-b053-e53cad63e8e8",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "eeb79ed3-d927-420e-9c55-cef8e63f496e");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Recipe_RecipeId",
                schema: "recipe",
                table: "Category",
                column: "RecipeId",
                principalSchema: "recipe",
                principalTable: "Recipe",
                principalColumn: "RecipeId");
        }
    }
}
