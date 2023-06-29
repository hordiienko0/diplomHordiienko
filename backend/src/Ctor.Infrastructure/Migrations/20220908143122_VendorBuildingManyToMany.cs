using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Ctor.Infrastructure.Migrations
{
    public partial class VendorBuildingManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BuildingVendor",
                columns: table => new
                {
                    BuildingsId = table.Column<long>(type: "bigint", nullable: false),
                    VendorsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildingVendor", x => new { x.BuildingsId, x.VendorsId });
                    table.ForeignKey(
                        name: "FK_BuildingVendor_Building_BuildingsId",
                        column: x => x.BuildingsId,
                        principalTable: "Building",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BuildingVendor_Vendor_VendorsId",
                        column: x => x.VendorsId,
                        principalTable: "Vendor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });          

            migrationBuilder.CreateIndex(
                name: "IX_BuildingVendor_VendorsId",
                table: "BuildingVendor",
                column: "VendorsId");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BuildingVendor");
        }
    }
}
