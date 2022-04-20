using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalFamilyCookbook.Migrations
{
    public partial class CorrectedNotesIdLength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                oldDefaultValue: "50ece01e-7e35-4191-8159-ec0d322aeef9");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "RecipeNote",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true,
                defaultValue: "3ce9b7f3-5047-40e8-90ef-f3f929b64fc1",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "f08052b7-ebd3-4400-bdd1-207274066a36");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "application",
                table: "Note",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true,
                defaultValue: "3b3b0140-b53f-4953-aa17-df375eb6ae81",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "ff2ce021-1b23-4b22-b520-4b409dffae03");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "InvitationCode",
                schema: "application",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "50ece01e-7e35-4191-8159-ec0d322aeef9",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "7e8f666c-19e6-45c7-800e-37a1260542e6");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "RecipeNote",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "f08052b7-ebd3-4400-bdd1-207274066a36",
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40,
                oldNullable: true,
                oldDefaultValue: "3ce9b7f3-5047-40e8-90ef-f3f929b64fc1");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "application",
                table: "Note",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "ff2ce021-1b23-4b22-b520-4b409dffae03",
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40,
                oldNullable: true,
                oldDefaultValue: "3b3b0140-b53f-4953-aa17-df375eb6ae81");
        }
    }
}
