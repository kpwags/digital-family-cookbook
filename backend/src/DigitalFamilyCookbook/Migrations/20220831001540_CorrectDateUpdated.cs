using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalFamilyCookbook.Migrations
{
    public partial class CorrectDateUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "InvitationCode",
                schema: "application",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "80bd1c38-433e-4649-b09e-1b29d8dac8d2",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "2c7d2c79-b256-4805-83ba-95d55f58ca32");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                oldDefaultValue: "80bd1c38-433e-4649-b09e-1b29d8dac8d2");
        }
    }
}
