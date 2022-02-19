using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalFamilyCookbook.Migrations
{
    public partial class UpdateSiteSettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshToken",
                schema: "application");

            migrationBuilder.AddColumn<bool>(
                name: "AllowPublicRegistration",
                schema: "application",
                table: "SiteSettings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "InvitationCode",
                schema: "application",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "3fe136b5-f9a9-450a-a326-7226d68430ee");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllowPublicRegistration",
                schema: "application",
                table: "SiteSettings");

            migrationBuilder.DropColumn(
                name: "InvitationCode",
                schema: "application",
                table: "SiteSettings");

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                schema: "application",
                columns: table => new
                {
                    RefreshTokenId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserAccountId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 1, 18, 17, 28, 29, 197, DateTimeKind.Local).AddTicks(7870)),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    JwtId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Token = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
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
        }
    }
}
