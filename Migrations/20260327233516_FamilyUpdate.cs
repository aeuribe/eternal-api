using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eternal_api.Migrations
{
    /// <inheritdoc />
    public partial class FamilyUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Code",
                table: "FAMILY",
                newName: "ShortName");

            migrationBuilder.AlterColumn<string>(
                name: "Sku",
                table: "FAMILY",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "FamilyCode",
                table: "FAMILY",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GenericCode",
                table: "FAMILY",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FamilyCode",
                table: "FAMILY");

            migrationBuilder.DropColumn(
                name: "GenericCode",
                table: "FAMILY");

            migrationBuilder.RenameColumn(
                name: "ShortName",
                table: "FAMILY",
                newName: "Code");

            migrationBuilder.AlterColumn<string>(
                name: "Sku",
                table: "FAMILY",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);
        }
    }
}
