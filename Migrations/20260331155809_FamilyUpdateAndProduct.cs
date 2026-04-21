using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eternal_api.Migrations
{
    /// <inheritdoc />
    public partial class FamilyUpdateAndProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GenericCode",
                table: "FAMILY");

            migrationBuilder.DropColumn(
                name: "Sku",
                table: "FAMILY");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "FAMILY");

            migrationBuilder.DropColumn(
                name: "Volume",
                table: "FAMILY");

            migrationBuilder.AddColumn<string>(
                name: "GenericCode",
                table: "PRODUCT",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Sku",
                table: "PRODUCT",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "PRODUCT",
                type: "character varying(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Volume",
                table: "PRODUCT",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GenericCode",
                table: "PRODUCT");

            migrationBuilder.DropColumn(
                name: "Sku",
                table: "PRODUCT");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "PRODUCT");

            migrationBuilder.DropColumn(
                name: "Volume",
                table: "PRODUCT");

            migrationBuilder.AddColumn<string>(
                name: "GenericCode",
                table: "FAMILY",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Sku",
                table: "FAMILY",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "FAMILY",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Volume",
                table: "FAMILY",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }
    }
}
