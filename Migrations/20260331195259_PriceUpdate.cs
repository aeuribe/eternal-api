using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eternal_api.Migrations
{
    /// <inheritdoc />
    public partial class PriceUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FamilyId",
                table: "HIST_PRICE",
                newName: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_HIST_PRICE_ProductId",
                table: "HIST_PRICE",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_HIST_PRICE_PRODUCT_ProductId",
                table: "HIST_PRICE",
                column: "ProductId",
                principalTable: "PRODUCT",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HIST_PRICE_PRODUCT_ProductId",
                table: "HIST_PRICE");

            migrationBuilder.DropIndex(
                name: "IX_HIST_PRICE_ProductId",
                table: "HIST_PRICE");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "HIST_PRICE",
                newName: "FamilyId");
        }
    }
}
