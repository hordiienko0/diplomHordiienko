using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ctor.Infrastructure.Migrations
{
    public partial class projectDocumentrework : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectDocument_Project_ProjectId",
                table: "ProjectDocument");

            migrationBuilder.DropIndex(
                name: "IX_ProjectDocument_DocumentId",
                table: "ProjectDocument");

            migrationBuilder.AlterColumn<long>(
                name: "ProjectId",
                table: "ProjectDocument",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "DocumentId",
                table: "ProjectDocument",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "BuildingId",
                table: "ProjectDocument",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "ProjectDocument",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "Document",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "ProjectDocumentId",
                table: "Document",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDocument_BuildingId",
                table: "ProjectDocument",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDocument_DocumentId",
                table: "ProjectDocument",
                column: "DocumentId",
                unique: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectDocument_Building_BuildingId",
                table: "ProjectDocument");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectDocument_Project_ProjectId",
                table: "ProjectDocument");

            migrationBuilder.DropIndex(
                name: "IX_ProjectDocument_BuildingId",
                table: "ProjectDocument");

            migrationBuilder.DropIndex(
                name: "IX_ProjectDocument_DocumentId",
                table: "ProjectDocument");

            migrationBuilder.DropColumn(
                name: "BuildingId",
                table: "ProjectDocument");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "ProjectDocument");

            migrationBuilder.DropColumn(
                name: "Link",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "ProjectDocumentId",
                table: "Document");

            migrationBuilder.AlterColumn<long>(
                name: "ProjectId",
                table: "ProjectDocument",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "DocumentId",
                table: "ProjectDocument",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDocument_DocumentId",
                table: "ProjectDocument",
                column: "DocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectDocument_Project_ProjectId",
                table: "ProjectDocument",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
