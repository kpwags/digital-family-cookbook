using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalFamilyCookbook.Migrations
{
    public partial class AddLandingPageText : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "InvitationCode",
                schema: "application",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "00c25f77-1812-470d-9df1-d3f4533f133f",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "80bd1c38-433e-4649-b09e-1b29d8dac8d2");

            migrationBuilder.AddColumn<string>(
                name: "LandingPageText",
                schema: "application",
                table: "SiteSettings",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LandingPageText",
                schema: "application",
                table: "SiteSettings");

            migrationBuilder.AlterColumn<string>(
                name: "InvitationCode",
                schema: "application",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "80bd1c38-433e-4649-b09e-1b29d8dac8d2",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "00c25f77-1812-470d-9df1-d3f4533f133f");
        }
    }
}
