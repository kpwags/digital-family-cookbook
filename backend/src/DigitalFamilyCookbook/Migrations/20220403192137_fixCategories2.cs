using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalFamilyCookbook.Migrations
{
    public partial class fixCategories2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                oldDefaultValue: "eeb79ed3-d927-420e-9c55-cef8e63f496e");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "InvitationCode",
                schema: "application",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "eeb79ed3-d927-420e-9c55-cef8e63f496e",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "23ab8c4e-4a2c-4e94-b448-8549371086cc");
        }
    }
}
