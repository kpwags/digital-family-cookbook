using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalFamilyCookbook.Migrations
{
    public partial class FixRecipeModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "Step",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "46dc5abe-dc32-4cbf-856e-88785c6c30a4",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "ab500f8f-cdba-4cb1-b350-546c0b414526");

            migrationBuilder.AlterColumn<string>(
                name: "InvitationCode",
                schema: "application",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "9a777178-0219-437d-b308-d05cc830fe5f",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "c4390086-41a9-46c0-b00d-9a8fa636642e");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "RecipeNote",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "a991238f-ff8c-4373-b9f1-662c160e87a4",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "3e25c600-8786-478e-aa9e-f342a835253d");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "RecipeMeat",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "1d4f6fd2-9e25-47d5-9fcb-112b39ac52c4",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "a73f4f42-de95-48eb-994c-ada78b302438");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "RecipeCategory",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "ac9179de-623b-4133-a9c0-c7175820bab7",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "b610aed5-3533-4d73-9af7-87af5863f12d");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "Recipe",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "dc54cd21-208f-4d32-8b61-7e5f7dd87c1b",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "2d03801b-3f84-43fd-8ddb-5f5a416ad2b5");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "application",
                table: "Note",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "fdfc0a31-9181-47f0-90d3-8972be69fa5d",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "9ebfb1f9-f4af-4208-b0ac-09bc0f779de5");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "Meat",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "90fd6a9e-10a8-4c9a-aeda-585b7ec71ebc",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "85d4ed16-9223-4f68-b77c-414f635a889f");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "Ingredient",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "5428d4c6-e116-4b95-ac30-86852ff0ec60",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "27970c6b-43e9-4b03-925d-c64483bd2f5a");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "Category",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "f86c1335-e973-4b37-a2b3-2473b4575639",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "18b5c727-7366-4d0d-a6e7-7728c251fb40");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "Step",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "ab500f8f-cdba-4cb1-b350-546c0b414526",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "46dc5abe-dc32-4cbf-856e-88785c6c30a4");

            migrationBuilder.AlterColumn<string>(
                name: "InvitationCode",
                schema: "application",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "c4390086-41a9-46c0-b00d-9a8fa636642e",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "9a777178-0219-437d-b308-d05cc830fe5f");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "RecipeNote",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "3e25c600-8786-478e-aa9e-f342a835253d",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "a991238f-ff8c-4373-b9f1-662c160e87a4");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "RecipeMeat",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "a73f4f42-de95-48eb-994c-ada78b302438",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "1d4f6fd2-9e25-47d5-9fcb-112b39ac52c4");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "RecipeCategory",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "b610aed5-3533-4d73-9af7-87af5863f12d",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "ac9179de-623b-4133-a9c0-c7175820bab7");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "Recipe",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "2d03801b-3f84-43fd-8ddb-5f5a416ad2b5",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "dc54cd21-208f-4d32-8b61-7e5f7dd87c1b");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "application",
                table: "Note",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "9ebfb1f9-f4af-4208-b0ac-09bc0f779de5",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "fdfc0a31-9181-47f0-90d3-8972be69fa5d");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "Meat",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "85d4ed16-9223-4f68-b77c-414f635a889f",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "90fd6a9e-10a8-4c9a-aeda-585b7ec71ebc");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "Ingredient",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "27970c6b-43e9-4b03-925d-c64483bd2f5a",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "5428d4c6-e116-4b95-ac30-86852ff0ec60");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "Category",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "18b5c727-7366-4d0d-a6e7-7728c251fb40",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "f86c1335-e973-4b37-a2b3-2473b4575639");
        }
    }
}
