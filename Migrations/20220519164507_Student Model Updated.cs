using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentWebapp.Migrations
{
    public partial class StudentModelUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Students_StudentId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_StudentId",
                table: "Students");

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StudentId1",
                table: "Students",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_StudentId1",
                table: "Students",
                column: "StudentId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Students_StudentId1",
                table: "Students",
                column: "StudentId1",
                principalTable: "Students",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Students_StudentId1",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_StudentId1",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StudentId1",
                table: "Students");

            migrationBuilder.AlterColumn<Guid>(
                name: "StudentId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Students_StudentId",
                table: "Students",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Students_StudentId",
                table: "Students",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }
    }
}
