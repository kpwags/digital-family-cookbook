using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalFamilyCookbook.Migrations
{
    public partial class CorrectMeatDateUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "InvitationCode",
                schema: "application",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "2c7d2c79-b256-4805-83ba-95d55f58ca32",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "9ace85e7-47ab-4a97-bb99-ce657723277f");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "InvitationCode",
                schema: "application",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "9ace85e7-47ab-4a97-bb99-ce657723277f",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "2c7d2c79-b256-4805-83ba-95d55f58ca32");
        }
    }
}
