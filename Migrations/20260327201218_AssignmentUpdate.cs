using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eternal_api.Migrations
{
    /// <inheritdoc />
    public partial class AssignmentUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ASSIGNMENT_USER_AssignedById",
                table: "ASSIGNMENT");

            migrationBuilder.DropIndex(
                name: "IX_ASSIGNMENT_AssignedById",
                table: "ASSIGNMENT");

            migrationBuilder.DropColumn(
                name: "AssignedById",
                table: "ASSIGNMENT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AssignedById",
                table: "ASSIGNMENT",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ASSIGNMENT_AssignedById",
                table: "ASSIGNMENT",
                column: "AssignedById");

            migrationBuilder.AddForeignKey(
                name: "FK_ASSIGNMENT_USER_AssignedById",
                table: "ASSIGNMENT",
                column: "AssignedById",
                principalTable: "USER",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
