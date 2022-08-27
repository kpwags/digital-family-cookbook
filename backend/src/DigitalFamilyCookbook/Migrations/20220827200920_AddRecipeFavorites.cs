using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalFamilyCookbook.Migrations
{
    public partial class AddRecipeFavorites : Migration
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
                defaultValue: "02d59620-abff-4cb0-8eab-18dd2ec40694",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "937f5ab5-3818-4750-8f22-8712f710f031");

            migrationBuilder.AlterColumn<string>(
                name: "InvitationCode",
                schema: "application",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "4bc85a11-7f1d-4c60-b5cd-0b758032148b",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "c5aaf2a9-c5e9-4fe4-9a85-7ab04e4a8c66");

            migrationBuilder.AlterColumn<string>(
                name: "ReplacedByToken",
                schema: "application",
                table: "RefreshToken",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ReasonRevoked",
                schema: "application",
                table: "RefreshToken",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "RecipeNote",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "fa70b9ab-1683-4b98-b963-10f950651e85",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "f2ef4df2-6b38-4801-a052-e74c4a9e0a88");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "RecipeMeat",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "27250ee7-e106-43c8-9b6a-960d42c1850e",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "01e6f38e-41a7-461a-8b56-a1ddc2a34ce8");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "RecipeCategory",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "9c2f2967-3b23-4622-bc62-db16966f7f4b",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "6d914464-f729-4d8d-9422-38c067bce8ec");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "Recipe",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "10b42890-1403-426f-89de-2813777732af",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "7adb1d17-3b81-474d-bc22-698b329d328f");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "application",
                table: "Note",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "83d3eb3e-f138-4afc-93a5-921d792a569d",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "069c3b5f-2060-4aab-9fb6-a6af2059c8f4");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "Meat",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "7690a0e7-fe30-4d6a-b646-0a60ab170480",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "9215c01e-b4a8-4157-919b-1e79741aee21");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "Ingredient",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "8e105d6d-e616-4283-8072-51fb78f4901a",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "e26f1477-c566-43da-ade8-f2ab21ad0f44");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "Category",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "83b22cf0-fde9-4ea4-95c2-8c74701d0396",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "781b0240-3501-4fd2-b883-dd437b3f5587");

            migrationBuilder.CreateTable(
                name: "RecipeFavorite",
                schema: "recipe",
                columns: table => new
                {
                    RecipeFavoriteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true, defaultValue: "a2c6f15d-3b10-4e91-af7a-4fa313ef0fee"),
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    UserAccountId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipe_RecipeFavorite", x => x.RecipeFavoriteId);
                    table.ForeignKey(
                        name: "FK_RecipeFavorite_Recipe_RecipeId",
                        column: x => x.RecipeId,
                        principalSchema: "recipe",
                        principalTable: "Recipe",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecipeFavorite_UserAccount_UserAccountId",
                        column: x => x.UserAccountId,
                        principalSchema: "application",
                        principalTable: "UserAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecipeFavorite_UserAccountId",
                schema: "recipe",
                table: "RecipeFavorite",
                column: "UserAccountId");

            migrationBuilder.CreateIndex(
                name: "UQ_Recipe_RecipeFavorite_RecipeId_UserAccountId",
                schema: "recipe",
                table: "RecipeFavorite",
                columns: new[] { "RecipeId", "UserAccountId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecipeFavorite",
                schema: "recipe");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "Step",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "937f5ab5-3818-4750-8f22-8712f710f031",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "02d59620-abff-4cb0-8eab-18dd2ec40694");

            migrationBuilder.AlterColumn<string>(
                name: "InvitationCode",
                schema: "application",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "c5aaf2a9-c5e9-4fe4-9a85-7ab04e4a8c66",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "4bc85a11-7f1d-4c60-b5cd-0b758032148b");

            migrationBuilder.AlterColumn<string>(
                name: "ReplacedByToken",
                schema: "application",
                table: "RefreshToken",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ReasonRevoked",
                schema: "application",
                table: "RefreshToken",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "RecipeNote",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "f2ef4df2-6b38-4801-a052-e74c4a9e0a88",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "fa70b9ab-1683-4b98-b963-10f950651e85");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "RecipeMeat",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "01e6f38e-41a7-461a-8b56-a1ddc2a34ce8",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "27250ee7-e106-43c8-9b6a-960d42c1850e");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "RecipeCategory",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "6d914464-f729-4d8d-9422-38c067bce8ec",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "9c2f2967-3b23-4622-bc62-db16966f7f4b");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "Recipe",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "7adb1d17-3b81-474d-bc22-698b329d328f",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "10b42890-1403-426f-89de-2813777732af");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "application",
                table: "Note",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "069c3b5f-2060-4aab-9fb6-a6af2059c8f4",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "83d3eb3e-f138-4afc-93a5-921d792a569d");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "Meat",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "9215c01e-b4a8-4157-919b-1e79741aee21",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "7690a0e7-fe30-4d6a-b646-0a60ab170480");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "Ingredient",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "e26f1477-c566-43da-ade8-f2ab21ad0f44",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "8e105d6d-e616-4283-8072-51fb78f4901a");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "Category",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "781b0240-3501-4fd2-b883-dd437b3f5587",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "83b22cf0-fde9-4ea4-95c2-8c74701d0396");
        }
    }
}
