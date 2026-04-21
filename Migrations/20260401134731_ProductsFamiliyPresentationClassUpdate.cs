using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eternal_api.Migrations
{
    /// <inheritdoc />
    public partial class ProductsFamiliyPresentationClassUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HIST_PRICE_PRODUCT_ProductId",
                table: "HIST_PRICE");

            migrationBuilder.DropForeignKey(
                name: "FK_PRODUCT_BRAND_BRAND_ID",
                table: "PRODUCT");

            migrationBuilder.DropIndex(
                name: "IX_PRODUCT_BRAND_ID",
                table: "PRODUCT");

            migrationBuilder.DropColumn(
                name: "BRAND_ID",
                table: "PRODUCT");

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

            migrationBuilder.RenameColumn(
                name: "ShortName",
                table: "PRODUCT",
                newName: "SHORT_NAME");

            migrationBuilder.RenameColumn(
                name: "FAMILY_ID",
                table: "PRODUCT",
                newName: "PRESENTATION_ID");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "HIST_PRICE",
                newName: "PresentationId");

            migrationBuilder.RenameIndex(
                name: "IX_HIST_PRICE_ProductId",
                table: "HIST_PRICE",
                newName: "IX_HIST_PRICE_PresentationId");

            migrationBuilder.AddColumn<Guid>(
                name: "BrandId",
                table: "FAMILY",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ClassId",
                table: "FAMILY",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "CLASS",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    NAME = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IS_ACTIVE = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLASS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PRESENTATION",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Sku = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    GenericCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Volume = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    Unit = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRESENTATION", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCT_PRESENTATION_ID",
                table: "PRODUCT",
                column: "PRESENTATION_ID");

            migrationBuilder.CreateIndex(
                name: "IX_FAMILY_BrandId",
                table: "FAMILY",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_FAMILY_ClassId",
                table: "FAMILY",
                column: "ClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_FAMILY_BRAND_BrandId",
                table: "FAMILY",
                column: "BrandId",
                principalTable: "BRAND",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FAMILY_CLASS_ClassId",
                table: "FAMILY",
                column: "ClassId",
                principalTable: "CLASS",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HIST_PRICE_PRESENTATION_PresentationId",
                table: "HIST_PRICE",
                column: "PresentationId",
                principalTable: "PRESENTATION",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PRODUCT_PRESENTATION_PRESENTATION_ID",
                table: "PRODUCT",
                column: "PRESENTATION_ID",
                principalTable: "PRESENTATION",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FAMILY_BRAND_BrandId",
                table: "FAMILY");

            migrationBuilder.DropForeignKey(
                name: "FK_FAMILY_CLASS_ClassId",
                table: "FAMILY");

            migrationBuilder.DropForeignKey(
                name: "FK_HIST_PRICE_PRESENTATION_PresentationId",
                table: "HIST_PRICE");

            migrationBuilder.DropForeignKey(
                name: "FK_PRODUCT_PRESENTATION_PRESENTATION_ID",
                table: "PRODUCT");

            migrationBuilder.DropTable(
                name: "CLASS");

            migrationBuilder.DropTable(
                name: "PRESENTATION");

            migrationBuilder.DropIndex(
                name: "IX_PRODUCT_PRESENTATION_ID",
                table: "PRODUCT");

            migrationBuilder.DropIndex(
                name: "IX_FAMILY_BrandId",
                table: "FAMILY");

            migrationBuilder.DropIndex(
                name: "IX_FAMILY_ClassId",
                table: "FAMILY");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "FAMILY");

            migrationBuilder.DropColumn(
                name: "ClassId",
                table: "FAMILY");

            migrationBuilder.RenameColumn(
                name: "SHORT_NAME",
                table: "PRODUCT",
                newName: "ShortName");

            migrationBuilder.RenameColumn(
                name: "PRESENTATION_ID",
                table: "PRODUCT",
                newName: "FAMILY_ID");

            migrationBuilder.RenameColumn(
                name: "PresentationId",
                table: "HIST_PRICE",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_HIST_PRICE_PresentationId",
                table: "HIST_PRICE",
                newName: "IX_HIST_PRICE_ProductId");

            migrationBuilder.AddColumn<Guid>(
                name: "BRAND_ID",
                table: "PRODUCT",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCT_BRAND_ID",
                table: "PRODUCT",
                column: "BRAND_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_HIST_PRICE_PRODUCT_ProductId",
                table: "HIST_PRICE",
                column: "ProductId",
                principalTable: "PRODUCT",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PRODUCT_BRAND_BRAND_ID",
                table: "PRODUCT",
                column: "BRAND_ID",
                principalTable: "BRAND",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
