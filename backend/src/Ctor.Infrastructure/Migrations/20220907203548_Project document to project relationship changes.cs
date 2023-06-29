using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ctor.Infrastructure.Migrations
{
    public partial class Projectdocumenttoprojectrelationshipchanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectDocument_Building_BuildingId",
                table: "ProjectDocument");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectDocument_Project_ProjectId",
                table: "ProjectDocument");

            migrationBuilder.AlterColumn<long>(
                name: "BuildingId",
                table: "ProjectDocument",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "ProjectDocumentId",
                table: "Document",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectDocument_Building_BuildingId",
                table: "ProjectDocument",
                column: "BuildingId",
                principalTable: "Building",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectDocument_Project_ProjectId",
                table: "ProjectDocument",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectDocument_Building_BuildingId",
                table: "ProjectDocument");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectDocument_Project_ProjectId",
                table: "ProjectDocument");

            migrationBuilder.AlterColumn<long>(
                name: "BuildingId",
                table: "ProjectDocument",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ProjectDocumentId",
                table: "Document",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectDocument_Building_BuildingId",
                table: "ProjectDocument",
                column: "BuildingId",
                principalTable: "Building",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectDocument_Project_ProjectId",
                table: "ProjectDocument",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id");
        }
    }
}
