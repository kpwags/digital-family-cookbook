using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalFamilyCookbook.Migrations
{
    public partial class AddSaveRecipeOnDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "InvitationCode",
                schema: "application",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "9d4780c1-21d4-4ea8-b053-e53cad63e8e8",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "3fe136b5-f9a9-450a-a326-7226d68430ee");

            migrationBuilder.AddColumn<bool>(
                name: "SaveRecipesOnDeleteUser",
                schema: "application",
                table: "SiteSettings",
                type: "bit",
                nullable: false,
                defaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SaveRecipesOnDeleteUser",
                schema: "application",
                table: "SiteSettings");

            migrationBuilder.AlterColumn<string>(
                name: "InvitationCode",
                schema: "application",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "3fe136b5-f9a9-450a-a326-7226d68430ee",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "9d4780c1-21d4-4ea8-b053-e53cad63e8e8");
        }
    }
}
