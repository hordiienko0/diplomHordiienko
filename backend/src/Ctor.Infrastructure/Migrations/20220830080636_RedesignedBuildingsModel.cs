using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ctor.Infrastructure.Migrations
{
    public partial class RedesignedBuildingsModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlockType",
                table: "Building");

            migrationBuilder.DropColumn(
                name: "BuildingType",
                table: "Building");

            migrationBuilder.AddColumn<bool>(
                name: "IsDone",
                table: "BuildingBlock",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "BuildingName",
                table: "Building",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDone",
                table: "BuildingBlock");

            migrationBuilder.DropColumn(
                name: "BuildingName",
                table: "Building");

            migrationBuilder.AddColumn<int>(
                name: "BlockType",
                table: "Building",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BuildingType",
                table: "Building",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
