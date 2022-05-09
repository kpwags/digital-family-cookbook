using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalFamilyCookbook.Migrations
{
    public partial class AddServingsToRecipe : Migration
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
                defaultValue: "8973a3a7-66aa-4438-a001-8323932a90cb",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "a72ca236-07ec-4050-8904-bf3a37fccf48");

            migrationBuilder.AlterColumn<string>(
                name: "InvitationCode",
                schema: "application",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "d95ea120-3601-4b45-820f-fcd65b215f43",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "da831ddb-d328-47fd-8107-1dd44c108057");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "RecipeStep",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "3e4eaab3-2d0b-4624-89d4-301ca6b559bd",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "fa9f3047-4893-469b-9627-1443b137339f");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "RecipeNote",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "6fd9b6e9-b8b7-4048-ab61-e330b618c42d",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "a8323f4b-24b7-4679-a339-0030446c6521");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "RecipeMeat",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "7ca08c49-9863-442f-8bdc-7610b4800525",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "319ac288-804b-430d-be80-b389b07e452a");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "RecipeIngredient",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "f17f4718-881f-4732-89d6-3ec0b93aaa25",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "9dfc5cb7-f226-428c-bca2-cd207d33acea");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "RecipeCategory",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "d8e21b77-4720-4bdd-a9f8-5d8d40d12335",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "e5701554-9bb5-45f5-9c2a-df51e6fdde2f");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "Recipe",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "08778354-f4e6-47d6-8f72-5f8e164590c9",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "c046b5d8-37eb-47d9-8be2-a9ebad7f2c19");

            migrationBuilder.AddColumn<int>(
                name: "Servings",
                schema: "recipe",
                table: "Recipe",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "application",
                table: "Note",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "0243aea4-9030-497b-8854-8cb1196585ca",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "f9be3ec4-7811-4d53-ba22-34cc2eda2df3");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "Meat",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "005a732b-00c3-4092-a37c-1ee126db15db",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "b3328805-aa4a-4fe9-9de5-a226e445a405");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "Ingredient",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "29bee1fb-e215-4e71-8cd4-a9d707f78087",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "69939e8f-9084-4e08-be12-d74ba569d111");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "Category",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "ef47924c-49d5-4436-8c28-d579ace9e489",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "11c4ff5e-9b32-4fea-9ddf-df4cb48117af");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Servings",
                schema: "recipe",
                table: "Recipe");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "Step",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "a72ca236-07ec-4050-8904-bf3a37fccf48",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "8973a3a7-66aa-4438-a001-8323932a90cb");

            migrationBuilder.AlterColumn<string>(
                name: "InvitationCode",
                schema: "application",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "da831ddb-d328-47fd-8107-1dd44c108057",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "d95ea120-3601-4b45-820f-fcd65b215f43");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "RecipeStep",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "fa9f3047-4893-469b-9627-1443b137339f",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "3e4eaab3-2d0b-4624-89d4-301ca6b559bd");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "RecipeNote",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "a8323f4b-24b7-4679-a339-0030446c6521",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "6fd9b6e9-b8b7-4048-ab61-e330b618c42d");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "RecipeMeat",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "319ac288-804b-430d-be80-b389b07e452a",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "7ca08c49-9863-442f-8bdc-7610b4800525");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "RecipeIngredient",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "9dfc5cb7-f226-428c-bca2-cd207d33acea",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "f17f4718-881f-4732-89d6-3ec0b93aaa25");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "RecipeCategory",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "e5701554-9bb5-45f5-9c2a-df51e6fdde2f",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "d8e21b77-4720-4bdd-a9f8-5d8d40d12335");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "Recipe",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "c046b5d8-37eb-47d9-8be2-a9ebad7f2c19",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "08778354-f4e6-47d6-8f72-5f8e164590c9");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "application",
                table: "Note",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "f9be3ec4-7811-4d53-ba22-34cc2eda2df3",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "0243aea4-9030-497b-8854-8cb1196585ca");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "Meat",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "b3328805-aa4a-4fe9-9de5-a226e445a405",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "005a732b-00c3-4092-a37c-1ee126db15db");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "Ingredient",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "69939e8f-9084-4e08-be12-d74ba569d111",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "29bee1fb-e215-4e71-8cd4-a9d707f78087");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "Category",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "11c4ff5e-9b32-4fea-9ddf-df4cb48117af",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "ef47924c-49d5-4436-8c28-d579ace9e489");
        }
    }
}
