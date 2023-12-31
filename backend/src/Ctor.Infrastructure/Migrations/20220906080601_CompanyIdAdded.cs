﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ctor.Infrastructure.Migrations
{
    public partial class CompanyIdAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendor_Company_CompanyId",
                table: "Vendor");

            migrationBuilder.AlterColumn<long>(
                name: "CompanyId",
                table: "Vendor",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Vendor_Company_CompanyId",
                table: "Vendor",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendor_Company_CompanyId",
                table: "Vendor");

            migrationBuilder.AlterColumn<long>(
                name: "CompanyId",
                table: "Vendor",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendor_Company_CompanyId",
                table: "Vendor",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id");
        }
    }
}
