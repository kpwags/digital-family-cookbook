using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalFamilyCookbook.Migrations
{
    public partial class AddRefreshTokens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Recipe_RecipeId",
                schema: "recipe",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Ingredient_Recipe_RecipeId",
                schema: "recipe",
                table: "Ingredient");

            migrationBuilder.DropForeignKey(
                name: "FK_Meat_Recipe_RecipeId",
                schema: "recipe",
                table: "Meat");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleType_Recipe_RecipeId",
                schema: "application",
                table: "RoleType");

            migrationBuilder.DropIndex(
                name: "IX_RoleType_RecipeId",
                schema: "application",
                table: "RoleType");

            migrationBuilder.DropColumn(
                name: "RecipeId",
                schema: "application",
                table: "RoleType");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                schema: "application",
                table: "UserAccountRoleType",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "application",
                table: "UserAccount",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "Step",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                schema: "application",
                table: "RoleType",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "RecipeStep",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "RecipeMeat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "RecipeIngredient",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "RecipeCategory",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "Recipe",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RecipeId",
                schema: "recipe",
                table: "Meat",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "Meat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RecipeId",
                schema: "recipe",
                table: "Ingredient",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "Ingredient",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RecipeId",
                schema: "recipe",
                table: "Category",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                schema: "recipe",
                table: "Category",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                schema: "application",
                columns: table => new
                {
                    RefreshTokenId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserAccountId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    JwtId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 1, 9, 10, 15, 55, 940, DateTimeKind.Local).AddTicks(5690)),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Application_RefreshToken_RefreshTokenId", x => x.RefreshTokenId);
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

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Recipe_RecipeId",
                schema: "recipe",
                table: "Category",
                column: "RecipeId",
                principalSchema: "recipe",
                principalTable: "Recipe",
                principalColumn: "RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredient_Recipe_RecipeId",
                schema: "recipe",
                table: "Ingredient",
                column: "RecipeId",
                principalSchema: "recipe",
                principalTable: "Recipe",
                principalColumn: "RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Meat_Recipe_RecipeId",
                schema: "recipe",
                table: "Meat",
                column: "RecipeId",
                principalSchema: "recipe",
                principalTable: "Recipe",
                principalColumn: "RecipeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Recipe_RecipeId",
                schema: "recipe",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Ingredient_Recipe_RecipeId",
                schema: "recipe",
                table: "Ingredient");

            migrationBuilder.DropForeignKey(
                name: "FK_Meat_Recipe_RecipeId",
                schema: "recipe",
                table: "Meat");

            migrationBuilder.DropTable(
                name: "RefreshToken",
                schema: "application");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "application",
                table: "UserAccountRoleType");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "application",
                table: "UserAccount");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "recipe",
                table: "Step");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "application",
                table: "RoleType");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "recipe",
                table: "RecipeStep");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "recipe",
                table: "RecipeMeat");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "recipe",
                table: "RecipeIngredient");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "recipe",
                table: "RecipeCategory");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "recipe",
                table: "Recipe");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "recipe",
                table: "Meat");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "recipe",
                table: "Ingredient");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "recipe",
                table: "Category");

            migrationBuilder.AddColumn<int>(
                name: "RecipeId",
                schema: "application",
                table: "RoleType",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RecipeId",
                schema: "recipe",
                table: "Meat",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RecipeId",
                schema: "recipe",
                table: "Ingredient",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RecipeId",
                schema: "recipe",
                table: "Category",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleType_RecipeId",
                schema: "application",
                table: "RoleType",
                column: "RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Recipe_RecipeId",
                schema: "recipe",
                table: "Category",
                column: "RecipeId",
                principalSchema: "recipe",
                principalTable: "Recipe",
                principalColumn: "RecipeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredient_Recipe_RecipeId",
                schema: "recipe",
                table: "Ingredient",
                column: "RecipeId",
                principalSchema: "recipe",
                principalTable: "Recipe",
                principalColumn: "RecipeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Meat_Recipe_RecipeId",
                schema: "recipe",
                table: "Meat",
                column: "RecipeId",
                principalSchema: "recipe",
                principalTable: "Recipe",
                principalColumn: "RecipeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleType_Recipe_RecipeId",
                schema: "application",
                table: "RoleType",
                column: "RecipeId",
                principalSchema: "recipe",
                principalTable: "Recipe",
                principalColumn: "RecipeId");
        }
    }
}
