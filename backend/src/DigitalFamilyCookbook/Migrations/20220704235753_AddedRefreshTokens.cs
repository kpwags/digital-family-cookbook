using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalFamilyCookbook.Migrations
{
    public partial class AddedRefreshTokens : Migration
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
                defaultValue: "937f5ab5-3818-4750-8f22-8712f710f031",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "73d2d360-4890-4a09-914f-0b5b6ff0f476");

            migrationBuilder.AlterColumn<string>(
                name: "InvitationCode",
                schema: "application",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "c5aaf2a9-c5e9-4fe4-9a85-7ab04e4a8c66",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "c8064ff3-7b83-4162-af30-d606fa553bfa");

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
                oldDefaultValue: "9e828120-49b9-4329-a4d8-2373c5f04a38");

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
                oldDefaultValue: "3adb04d3-4ac2-4cb8-8879-65c110110ff9");

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
                oldDefaultValue: "0fde9d3e-3b4c-43cc-99c1-9366d4abb230");

            migrationBuilder.AlterColumn<string>(
                name: "SourceUrl",
                schema: "recipe",
                table: "Recipe",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Source",
                schema: "recipe",
                table: "Recipe",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrlLarge",
                schema: "recipe",
                table: "Recipe",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                schema: "recipe",
                table: "Recipe",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

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
                oldDefaultValue: "b022e383-379a-44e9-bd0a-25a2ac5b6fa3");

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
                oldDefaultValue: "63978d2a-7a1e-45ca-ab26-2478302fd763");

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
                oldDefaultValue: "1689ae3e-1e6d-46d2-b31a-e695ee80b49a");

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
                oldDefaultValue: "337cc394-83e2-4a8a-8a7d-e55712fa2673");

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
                oldDefaultValue: "da9a6fdd-d702-4dc3-bc9f-6caea2c9e410");

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                schema: "application",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Expires = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByIp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Revoked = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RevokedByIp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReplacedByToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReasonRevoked = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserAccountId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshToken_UserAccount_UserAccountId",
                        column: x => x.UserAccountId,
                        principalSchema: "application",
                        principalTable: "UserAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_UserAccountId",
                schema: "application",
                table: "RefreshToken",
                column: "UserAccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshToken",
                schema: "application");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "Step",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "73d2d360-4890-4a09-914f-0b5b6ff0f476",
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
                defaultValue: "c8064ff3-7b83-4162-af30-d606fa553bfa",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "c5aaf2a9-c5e9-4fe4-9a85-7ab04e4a8c66");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "RecipeNote",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "9e828120-49b9-4329-a4d8-2373c5f04a38",
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
                defaultValue: "3adb04d3-4ac2-4cb8-8879-65c110110ff9",
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
                defaultValue: "0fde9d3e-3b4c-43cc-99c1-9366d4abb230",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "6d914464-f729-4d8d-9422-38c067bce8ec");

            migrationBuilder.AlterColumn<string>(
                name: "SourceUrl",
                schema: "recipe",
                table: "Recipe",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Source",
                schema: "recipe",
                table: "Recipe",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrlLarge",
                schema: "recipe",
                table: "Recipe",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                schema: "recipe",
                table: "Recipe",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "Recipe",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                defaultValue: "b022e383-379a-44e9-bd0a-25a2ac5b6fa3",
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
                defaultValue: "63978d2a-7a1e-45ca-ab26-2478302fd763",
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
                defaultValue: "1689ae3e-1e6d-46d2-b31a-e695ee80b49a",
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
                defaultValue: "337cc394-83e2-4a8a-8a7d-e55712fa2673",
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
                defaultValue: "da9a6fdd-d702-4dc3-bc9f-6caea2c9e410",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true,
                oldDefaultValue: "781b0240-3501-4fd2-b883-dd437b3f5587");
        }
    }
}
