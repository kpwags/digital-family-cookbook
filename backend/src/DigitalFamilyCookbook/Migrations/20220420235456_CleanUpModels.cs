using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalFamilyCookbook.Migrations
{
    public partial class CleanUpModels : Migration
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
                defaultValue: "a72ca236-07ec-4050-8904-bf3a37fccf48",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                schema: "recipe",
                table: "Step",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateUpdated",
                schema: "recipe",
                table: "Step",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<string>(
                name: "InvitationCode",
                schema: "application",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "da831ddb-d328-47fd-8107-1dd44c108057",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "c1f74e33-c1a4-486b-89c9-b6e22abcd16c");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "RecipeStep",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "fa9f3047-4893-469b-9627-1443b137339f",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                schema: "recipe",
                table: "RecipeStep",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateUpdated",
                schema: "recipe",
                table: "RecipeStep",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");

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
                oldDefaultValue: "cce0c4fc-dd1e-4e20-92c3-084cf770677f");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "RecipeMeat",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "319ac288-804b-430d-be80-b389b07e452a",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                schema: "recipe",
                table: "RecipeMeat",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateUpdated",
                schema: "recipe",
                table: "RecipeMeat",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "RecipeIngredient",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "9dfc5cb7-f226-428c-bca2-cd207d33acea",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                schema: "recipe",
                table: "RecipeIngredient",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateUpdated",
                schema: "recipe",
                table: "RecipeIngredient",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "RecipeCategory",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "e5701554-9bb5-45f5-9c2a-df51e6fdde2f",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                schema: "recipe",
                table: "RecipeCategory",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateUpdated",
                schema: "recipe",
                table: "RecipeCategory",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "Recipe",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "c046b5d8-37eb-47d9-8be2-a9ebad7f2c19",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                schema: "recipe",
                table: "Recipe",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateUpdated",
                schema: "recipe",
                table: "Recipe",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");

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
                oldDefaultValue: "588969b3-e164-44ca-a456-c6f843935bda");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "Meat",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "b3328805-aa4a-4fe9-9de5-a226e445a405",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                schema: "recipe",
                table: "Meat",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateUpdated",
                schema: "recipe",
                table: "Meat",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "Ingredient",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "69939e8f-9084-4e08-be12-d74ba569d111",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                schema: "recipe",
                table: "Ingredient",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateUpdated",
                schema: "recipe",
                table: "Ingredient",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "Category",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "11c4ff5e-9b32-4fea-9ddf-df4cb48117af",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                schema: "recipe",
                table: "Category",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateUpdated",
                schema: "recipe",
                table: "Category",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                schema: "recipe",
                table: "Step");

            migrationBuilder.DropColumn(
                name: "DateUpdated",
                schema: "recipe",
                table: "Step");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                schema: "recipe",
                table: "RecipeStep");

            migrationBuilder.DropColumn(
                name: "DateUpdated",
                schema: "recipe",
                table: "RecipeStep");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                schema: "recipe",
                table: "RecipeMeat");

            migrationBuilder.DropColumn(
                name: "DateUpdated",
                schema: "recipe",
                table: "RecipeMeat");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                schema: "recipe",
                table: "RecipeIngredient");

            migrationBuilder.DropColumn(
                name: "DateUpdated",
                schema: "recipe",
                table: "RecipeIngredient");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                schema: "recipe",
                table: "RecipeCategory");

            migrationBuilder.DropColumn(
                name: "DateUpdated",
                schema: "recipe",
                table: "RecipeCategory");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                schema: "recipe",
                table: "Recipe");

            migrationBuilder.DropColumn(
                name: "DateUpdated",
                schema: "recipe",
                table: "Recipe");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                schema: "recipe",
                table: "Meat");

            migrationBuilder.DropColumn(
                name: "DateUpdated",
                schema: "recipe",
                table: "Meat");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                schema: "recipe",
                table: "Ingredient");

            migrationBuilder.DropColumn(
                name: "DateUpdated",
                schema: "recipe",
                table: "Ingredient");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                schema: "recipe",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "DateUpdated",
                schema: "recipe",
                table: "Category");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "Step",
                type: "nvarchar(max)",
                nullable: true,
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
                defaultValue: "c1f74e33-c1a4-486b-89c9-b6e22abcd16c",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "da831ddb-d328-47fd-8107-1dd44c108057");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "RecipeStep",
                type: "nvarchar(max)",
                nullable: true,
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
                defaultValue: "cce0c4fc-dd1e-4e20-92c3-084cf770677f",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "a8323f4b-24b7-4679-a339-0030446c6521");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "RecipeMeat",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "319ac288-804b-430d-be80-b389b07e452a");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "RecipeIngredient",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "9dfc5cb7-f226-428c-bca2-cd207d33acea");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "RecipeCategory",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "e5701554-9bb5-45f5-9c2a-df51e6fdde2f");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "Recipe",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "c046b5d8-37eb-47d9-8be2-a9ebad7f2c19");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "application",
                table: "Note",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "588969b3-e164-44ca-a456-c6f843935bda",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "f9be3ec4-7811-4d53-ba22-34cc2eda2df3");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "Meat",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "b3328805-aa4a-4fe9-9de5-a226e445a405");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "Ingredient",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "69939e8f-9084-4e08-be12-d74ba569d111");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "Category",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "11c4ff5e-9b32-4fea-9ddf-df4cb48117af");
        }
    }
}
