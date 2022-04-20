using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalFamilyCookbook.Migrations
{
    public partial class CorrectedNotesIdLength2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "InvitationCode",
                schema: "application",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "c1f74e33-c1a4-486b-89c9-b6e22abcd16c",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "7e8f666c-19e6-45c7-800e-37a1260542e6");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "RecipeNote",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "cce0c4fc-dd1e-4e20-92c3-084cf770677f",
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40,
                oldNullable: true,
                oldDefaultValue: "3ce9b7f3-5047-40e8-90ef-f3f929b64fc1");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "application",
                table: "Note",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "588969b3-e164-44ca-a456-c6f843935bda",
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40,
                oldNullable: true,
                oldDefaultValue: "3b3b0140-b53f-4953-aa17-df375eb6ae81");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "InvitationCode",
                schema: "application",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "7e8f666c-19e6-45c7-800e-37a1260542e6",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "c1f74e33-c1a4-486b-89c9-b6e22abcd16c");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "RecipeNote",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true,
                defaultValue: "3ce9b7f3-5047-40e8-90ef-f3f929b64fc1",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "cce0c4fc-dd1e-4e20-92c3-084cf770677f");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "application",
                table: "Note",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true,
                defaultValue: "3b3b0140-b53f-4953-aa17-df375eb6ae81",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "588969b3-e164-44ca-a456-c6f843935bda");
        }
    }
}
