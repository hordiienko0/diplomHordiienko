using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Ctor.Infrastructure.Migrations
{
    public partial class AddResourcesMaterial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecourceName",
                table: "Material");

            migrationBuilder.DropColumn(
                name: "RecourceType",
                table: "Material");

            migrationBuilder.AddColumn<string>(
                name: "CompanyAddress",
                table: "Material",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Material",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Material",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "MaterialTypeId",
                table: "Material",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "MeasurementId",
                table: "Material",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MaterialType",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'100', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Measurement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'100', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measurement", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Material_MaterialTypeId",
                table: "Material",
                column: "MaterialTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Material_MeasurementId",
                table: "Material",
                column: "MeasurementId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialType_Id",
                table: "MaterialType",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Measurement_Id",
                table: "Measurement",
                column: "Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Material_MaterialType_MaterialTypeId",
                table: "Material",
                column: "MaterialTypeId",
                principalTable: "MaterialType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Material_Measurement_MeasurementId",
                table: "Material",
                column: "MeasurementId",
                principalTable: "Measurement",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Material_MaterialType_MaterialTypeId",
                table: "Material");

            migrationBuilder.DropForeignKey(
                name: "FK_Material_Measurement_MeasurementId",
                table: "Material");

            migrationBuilder.DropTable(
                name: "MaterialType");

            migrationBuilder.DropTable(
                name: "Measurement");

            migrationBuilder.DropIndex(
                name: "IX_Material_MaterialTypeId",
                table: "Material");

            migrationBuilder.DropIndex(
                name: "IX_Material_MeasurementId",
                table: "Material");

            migrationBuilder.DropColumn(
                name: "CompanyAddress",
                table: "Material");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Material");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Material");

            migrationBuilder.DropColumn(
                name: "MaterialTypeId",
                table: "Material");

            migrationBuilder.DropColumn(
                name: "MeasurementId",
                table: "Material");

            migrationBuilder.AddColumn<int>(
                name: "RecourceName",
                table: "Material",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RecourceType",
                table: "Material",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
