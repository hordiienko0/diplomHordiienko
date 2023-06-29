using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Ctor.Infrastructure.Migrations
{
    public partial class VendorAndVendorTypeCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendor_Company_CompanyId",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "VendorType",
                table: "Vendor");

            migrationBuilder.RenameColumn(
                name: "EntityName",
                table: "Vendor",
                newName: "VendorName");

            migrationBuilder.AlterColumn<long>(
                name: "CompanyId",
                table: "Vendor",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateTable(
                name: "VendorType",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'100', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VendorVendorType",
                columns: table => new
                {
                    VendorTypesId = table.Column<long>(type: "bigint", nullable: false),
                    VendorsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorVendorType", x => new { x.VendorTypesId, x.VendorsId });
                    table.ForeignKey(
                        name: "FK_VendorVendorType_Vendor_VendorsId",
                        column: x => x.VendorsId,
                        principalTable: "Vendor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VendorVendorType_VendorType_VendorTypesId",
                        column: x => x.VendorTypesId,
                        principalTable: "VendorType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VendorType_Id",
                table: "VendorType",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VendorVendorType_VendorsId",
                table: "VendorVendorType",
                column: "VendorsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendor_Company_CompanyId",
                table: "Vendor",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendor_Company_CompanyId",
                table: "Vendor");

            migrationBuilder.DropTable(
                name: "VendorVendorType");

            migrationBuilder.DropTable(
                name: "VendorType");

            migrationBuilder.RenameColumn(
                name: "VendorName",
                table: "Vendor",
                newName: "EntityName");

            migrationBuilder.AlterColumn<long>(
                name: "CompanyId",
                table: "Vendor",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Vendor",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "VendorType",
                table: "Vendor",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Vendor_Company_CompanyId",
                table: "Vendor",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
