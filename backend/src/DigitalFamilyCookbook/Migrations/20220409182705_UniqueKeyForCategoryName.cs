using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalFamilyCookbook.Migrations
{
    public partial class UniqueKeyForCategoryName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                oldDefaultValue: "848b550d-74a3-4d3a-8ccf-35139d087f5c");

            migrationBuilder.CreateIndex(
                name: "UQ_Recipe_Category_Name",
                schema: "recipe",
                table: "Category",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ_Recipe_Category_Name",
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
                oldDefaultValue: "62fa4988-9a70-4c81-b51e-f67cda34691e");
        }
    }
}
