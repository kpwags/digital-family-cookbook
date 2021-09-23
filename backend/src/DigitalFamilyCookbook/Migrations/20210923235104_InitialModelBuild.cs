using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DigitalFamilyCookbook.Migrations
{
    public partial class InitialModelBuild : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "recipe");

            migrationBuilder.EnsureSchema(
                name: "application");

            migrationBuilder.CreateTable(
                name: "UserAccount",
                schema: "application",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccount", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recipe",
                schema: "recipe",
                columns: table => new
                {
                    RecipeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Notes = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Source = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    SourceUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Time = table.Column<int>(type: "int", nullable: true),
                    ActiveTime = table.Column<int>(type: "int", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ImageUrlLarge = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Calories = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    Carbohydrates = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    Sugar = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    Fat = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    Protein = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    Fiber = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    Cholesterol = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    UserAccountId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipe_Recipe_RecipeId", x => x.RecipeId);
                    table.ForeignKey(
                        name: "FK_Recipe_UserAccount_UserAccountId",
                        column: x => x.UserAccountId,
                        principalSchema: "application",
                        principalTable: "UserAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                schema: "recipe",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipe_Category_CategoryId", x => x.CategoryId);
                    table.ForeignKey(
                        name: "FK_Category_Recipe_RecipeId",
                        column: x => x.RecipeId,
                        principalSchema: "recipe",
                        principalTable: "Recipe",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ingredient",
                schema: "recipe",
                columns: table => new
                {
                    IngredientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: true),
                    RecipeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipe_Ingredient_IngredientId", x => x.IngredientId);
                    table.ForeignKey(
                        name: "FK_Ingredient_Recipe_RecipeId",
                        column: x => x.RecipeId,
                        principalSchema: "recipe",
                        principalTable: "Recipe",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Meat",
                schema: "recipe",
                columns: table => new
                {
                    MeatId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipe_Meat_MeatyId", x => x.MeatId);
                    table.ForeignKey(
                        name: "FK_Meat_Recipe_RecipeId",
                        column: x => x.RecipeId,
                        principalSchema: "recipe",
                        principalTable: "Recipe",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleType",
                schema: "application",
                columns: table => new
                {
                    RoleTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: true),
                    UserAccountId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Application_RoleType_RoleTypeId", x => x.RoleTypeId);
                    table.ForeignKey(
                        name: "FK_RoleType_Recipe_RecipeId",
                        column: x => x.RecipeId,
                        principalSchema: "recipe",
                        principalTable: "Recipe",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RoleType_UserAccount_UserAccountId",
                        column: x => x.UserAccountId,
                        principalSchema: "application",
                        principalTable: "UserAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Step",
                schema: "recipe",
                columns: table => new
                {
                    StepId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Direction = table.Column<string>(type: "VARCHAR(MAX)", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipe_Step_StepId", x => x.StepId);
                    table.ForeignKey(
                        name: "FK_Step_Recipe_RecipeId",
                        column: x => x.RecipeId,
                        principalSchema: "recipe",
                        principalTable: "Recipe",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipeCategory",
                schema: "recipe",
                columns: table => new
                {
                    RecipeCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipe_RecipeCategory_RecipeCategoryId", x => x.RecipeCategoryId);
                    table.ForeignKey(
                        name: "FK_RecipeCategory_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "recipe",
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecipeCategory_Recipe_RecipeId",
                        column: x => x.RecipeId,
                        principalSchema: "recipe",
                        principalTable: "Recipe",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RecipeIngredient",
                schema: "recipe",
                columns: table => new
                {
                    RecipeIngredientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    IngredientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipe_RecipeIngredient_RecipeIngredientId", x => x.RecipeIngredientId);
                    table.ForeignKey(
                        name: "FK_RecipeIngredient_Ingredient_IngredientId",
                        column: x => x.IngredientId,
                        principalSchema: "recipe",
                        principalTable: "Ingredient",
                        principalColumn: "IngredientId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecipeIngredient_Recipe_RecipeId",
                        column: x => x.RecipeId,
                        principalSchema: "recipe",
                        principalTable: "Recipe",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RecipeMeat",
                schema: "recipe",
                columns: table => new
                {
                    RecipeMeatId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    MeatId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipe_RecipeMeat_RecipeMeatId", x => x.RecipeMeatId);
                    table.ForeignKey(
                        name: "FK_RecipeMeat_Meat_MeatId",
                        column: x => x.MeatId,
                        principalSchema: "recipe",
                        principalTable: "Meat",
                        principalColumn: "MeatId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecipeMeat_Recipe_RecipeId",
                        column: x => x.RecipeId,
                        principalSchema: "recipe",
                        principalTable: "Recipe",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserAccountRoleType",
                schema: "application",
                columns: table => new
                {
                    UserAccountRoleTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserAccountId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Application_UserAccountRoleType_UserAccountRoleTypeId", x => x.UserAccountRoleTypeId);
                    table.ForeignKey(
                        name: "FK_UserAccountRoleType_RoleType_RoleTypeId",
                        column: x => x.RoleTypeId,
                        principalSchema: "application",
                        principalTable: "RoleType",
                        principalColumn: "RoleTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserAccountRoleType_UserAccount_UserAccountId",
                        column: x => x.UserAccountId,
                        principalSchema: "application",
                        principalTable: "UserAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RecipeStep",
                schema: "recipe",
                columns: table => new
                {
                    RecipeStepId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    StepId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipe_RecipeStep_RecipeStepId", x => x.RecipeStepId);
                    table.ForeignKey(
                        name: "FK_RecipeStep_Recipe_RecipeId",
                        column: x => x.RecipeId,
                        principalSchema: "recipe",
                        principalTable: "Recipe",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecipeStep_Step_StepId",
                        column: x => x.StepId,
                        principalSchema: "recipe",
                        principalTable: "Step",
                        principalColumn: "StepId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Category_RecipeId",
                schema: "recipe",
                table: "Category",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredient_RecipeId",
                schema: "recipe",
                table: "Ingredient",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Meat_RecipeId",
                schema: "recipe",
                table: "Meat",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_UserAccountId",
                schema: "recipe",
                table: "Recipe",
                column: "UserAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeCategory_CategoryId",
                schema: "recipe",
                table: "RecipeCategory",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "UQ_Recipe_RecipeCategory_RecipeId_CategoryId",
                schema: "recipe",
                table: "RecipeCategory",
                columns: new[] { "RecipeId", "CategoryId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredient_IngredientId",
                schema: "recipe",
                table: "RecipeIngredient",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "UQ_Recipe_RecipeIngredient_RecipeId_IngredientId",
                schema: "recipe",
                table: "RecipeIngredient",
                columns: new[] { "RecipeId", "IngredientId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecipeMeat_MeatId",
                schema: "recipe",
                table: "RecipeMeat",
                column: "MeatId");

            migrationBuilder.CreateIndex(
                name: "UQ_Recipe_RecipeMeat_RecipeId_MeatId",
                schema: "recipe",
                table: "RecipeMeat",
                columns: new[] { "RecipeId", "MeatId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecipeStep_StepId",
                schema: "recipe",
                table: "RecipeStep",
                column: "StepId");

            migrationBuilder.CreateIndex(
                name: "UQ_Recipe_RecipeStep_RecipeId_StepId",
                schema: "recipe",
                table: "RecipeStep",
                columns: new[] { "RecipeId", "StepId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleType_RecipeId",
                schema: "application",
                table: "RoleType",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleType_UserAccountId",
                schema: "application",
                table: "RoleType",
                column: "UserAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Step_RecipeId",
                schema: "recipe",
                table: "Step",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccountRoleType_RoleTypeId",
                schema: "application",
                table: "UserAccountRoleType",
                column: "RoleTypeId");

            migrationBuilder.CreateIndex(
                name: "UQ_Application_UserAccountRoleType_UserAccountId_RoleTypeId",
                schema: "application",
                table: "UserAccountRoleType",
                columns: new[] { "UserAccountId", "RoleTypeId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecipeCategory",
                schema: "recipe");

            migrationBuilder.DropTable(
                name: "RecipeIngredient",
                schema: "recipe");

            migrationBuilder.DropTable(
                name: "RecipeMeat",
                schema: "recipe");

            migrationBuilder.DropTable(
                name: "RecipeStep",
                schema: "recipe");

            migrationBuilder.DropTable(
                name: "UserAccountRoleType",
                schema: "application");

            migrationBuilder.DropTable(
                name: "Category",
                schema: "recipe");

            migrationBuilder.DropTable(
                name: "Ingredient",
                schema: "recipe");

            migrationBuilder.DropTable(
                name: "Meat",
                schema: "recipe");

            migrationBuilder.DropTable(
                name: "Step",
                schema: "recipe");

            migrationBuilder.DropTable(
                name: "RoleType",
                schema: "application");

            migrationBuilder.DropTable(
                name: "Recipe",
                schema: "recipe");

            migrationBuilder.DropTable(
                name: "UserAccount",
                schema: "application");
        }
    }
}
