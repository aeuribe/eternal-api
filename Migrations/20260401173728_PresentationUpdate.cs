using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eternal_api.Migrations
{
    /// <inheritdoc />
    public partial class PresentationUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FamilyId",
                table: "PRESENTATION",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_PRESENTATION_FamilyId",
                table: "PRESENTATION",
                column: "FamilyId");

            migrationBuilder.AddForeignKey(
                name: "FK_PRESENTATION_FAMILY_FamilyId",
                table: "PRESENTATION",
                column: "FamilyId",
                principalTable: "FAMILY",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PRESENTATION_FAMILY_FamilyId",
                table: "PRESENTATION");

            migrationBuilder.DropIndex(
                name: "IX_PRESENTATION_FamilyId",
                table: "PRESENTATION");

            migrationBuilder.DropColumn(
                name: "FamilyId",
                table: "PRESENTATION");
        }
    }
}
