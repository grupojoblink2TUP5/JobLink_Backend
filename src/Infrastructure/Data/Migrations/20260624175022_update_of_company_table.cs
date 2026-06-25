using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class update_of_company_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Location",
                table: "Companies",
                newName: "Sector");

            migrationBuilder.RenameColumn(
                name: "Industry",
                table: "Companies",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "Approved",
                table: "Companies",
                newName: "Status");

            migrationBuilder.AlterColumn<string>(
                name: "Website",
                table: "Companies",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Companies",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedAt",
                table: "Companies",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApprovedByAdminId",
                table: "Companies",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByRecruiterId",
                table: "Companies",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ImagePublicId",
                table: "Companies",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Companies",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedAt",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "ApprovedByAdminId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "CreatedByRecruiterId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "ImagePublicId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Companies");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Companies",
                newName: "Approved");

            migrationBuilder.RenameColumn(
                name: "Sector",
                table: "Companies",
                newName: "Location");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Companies",
                newName: "Industry");

            migrationBuilder.AlterColumn<string>(
                name: "Website",
                table: "Companies",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Companies",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }
    }
}
