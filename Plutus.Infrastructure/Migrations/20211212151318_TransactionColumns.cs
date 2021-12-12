using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plutus.Infrastructure.Migrations
{
    public partial class TransactionColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Transaction",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Transaction",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Transaction");
        }
    }
}
