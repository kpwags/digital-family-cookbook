using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalFamilyCookbook.Migrations
{
    public partial class AddSystemSettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedDate",
                schema: "application",
                table: "RefreshToken",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 14, 20, 34, 9, 33, DateTimeKind.Local).AddTicks(4570),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 1, 10, 20, 12, 26, 446, DateTimeKind.Local).AddTicks(6990));

            migrationBuilder.CreateTable(
                name: "SiteSettings",
                schema: "application",
                columns: table => new
                {
                    SiteSettingsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Application_SiteSettings_SiteSettingsId", x => x.SiteSettingsId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SiteSettings",
                schema: "application");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedDate",
                schema: "application",
                table: "RefreshToken",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 10, 20, 12, 26, 446, DateTimeKind.Local).AddTicks(6990),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 1, 14, 20, 34, 9, 33, DateTimeKind.Local).AddTicks(4570));
        }
    }
}
