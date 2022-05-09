using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalFamilyCookbook.Migrations
{
    public partial class FixCategoryAndMeatLinks : Migration
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
                defaultValue: "100b2cd5-4b3a-4adf-94d2-1802162fe62a",
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
                defaultValue: "3ed80584-2e04-430d-a13f-713397ddc229",
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
                defaultValue: "a13ea28c-5a8f-4790-bc04-0f2f5b2619f5",
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
                defaultValue: "1d269492-ef8b-45e0-8fec-472a6e454ac1",
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
                defaultValue: "4dce1f7e-cd40-4533-b07d-d4b404702bcb",
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
                defaultValue: "cf175c7f-4bed-4aaa-837c-0311f295abd1",
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
                defaultValue: "0cb26e31-75b7-4a77-b6da-58a57fd9ae9c",
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
                defaultValue: "ad48f140-ea91-4cb7-be9d-4c6294fc02cb",
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
                defaultValue: "0b0e803c-6a00-40eb-9d92-aac0bb65811e",
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
                defaultValue: "3e9dadc1-a566-4726-85d7-e4ece376622c",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "f86c1335-e973-4b37-a2b3-2473b4575639");
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
                defaultValue: "46dc5abe-dc32-4cbf-856e-88785c6c30a4",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "100b2cd5-4b3a-4adf-94d2-1802162fe62a");

            migrationBuilder.AlterColumn<string>(
                name: "InvitationCode",
                schema: "application",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "9a777178-0219-437d-b308-d05cc830fe5f",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "3ed80584-2e04-430d-a13f-713397ddc229");

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
                oldDefaultValue: "a13ea28c-5a8f-4790-bc04-0f2f5b2619f5");

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
                oldDefaultValue: "1d269492-ef8b-45e0-8fec-472a6e454ac1");

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
                oldDefaultValue: "4dce1f7e-cd40-4533-b07d-d4b404702bcb");

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
                oldDefaultValue: "cf175c7f-4bed-4aaa-837c-0311f295abd1");

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
                oldDefaultValue: "0cb26e31-75b7-4a77-b6da-58a57fd9ae9c");

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
                oldDefaultValue: "ad48f140-ea91-4cb7-be9d-4c6294fc02cb");

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
                oldDefaultValue: "0b0e803c-6a00-40eb-9d92-aac0bb65811e");

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
                oldDefaultValue: "3e9dadc1-a566-4726-85d7-e4ece376622c");
        }
    }
}
