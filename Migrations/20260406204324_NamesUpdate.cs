using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eternal_api.Migrations
{
    /// <inheritdoc />
    public partial class NamesUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Districts_Regions_RegionId",
                table: "Districts");

            migrationBuilder.DropForeignKey(
                name: "FK_Regions_Areas_AreaId",
                table: "Regions");

            migrationBuilder.DropForeignKey(
                name: "FK_STORE_Districts_DistrictId",
                table: "STORE");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Regions",
                table: "Regions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Districts",
                table: "Districts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Areas",
                table: "Areas");

            migrationBuilder.RenameTable(
                name: "Regions",
                newName: "REGION");

            migrationBuilder.RenameTable(
                name: "Districts",
                newName: "DISTRICT");

            migrationBuilder.RenameTable(
                name: "Areas",
                newName: "AREA");

            migrationBuilder.RenameIndex(
                name: "IX_Regions_AreaId",
                table: "REGION",
                newName: "IX_REGION_AreaId");

            migrationBuilder.RenameIndex(
                name: "IX_Districts_RegionId",
                table: "DISTRICT",
                newName: "IX_DISTRICT_RegionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_REGION",
                table: "REGION",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DISTRICT",
                table: "DISTRICT",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AREA",
                table: "AREA",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DISTRICT_REGION_RegionId",
                table: "DISTRICT",
                column: "RegionId",
                principalTable: "REGION",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_REGION_AREA_AreaId",
                table: "REGION",
                column: "AreaId",
                principalTable: "AREA",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_STORE_DISTRICT_DistrictId",
                table: "STORE",
                column: "DistrictId",
                principalTable: "DISTRICT",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DISTRICT_REGION_RegionId",
                table: "DISTRICT");

            migrationBuilder.DropForeignKey(
                name: "FK_REGION_AREA_AreaId",
                table: "REGION");

            migrationBuilder.DropForeignKey(
                name: "FK_STORE_DISTRICT_DistrictId",
                table: "STORE");

            migrationBuilder.DropPrimaryKey(
                name: "PK_REGION",
                table: "REGION");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DISTRICT",
                table: "DISTRICT");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AREA",
                table: "AREA");

            migrationBuilder.RenameTable(
                name: "REGION",
                newName: "Regions");

            migrationBuilder.RenameTable(
                name: "DISTRICT",
                newName: "Districts");

            migrationBuilder.RenameTable(
                name: "AREA",
                newName: "Areas");

            migrationBuilder.RenameIndex(
                name: "IX_REGION_AreaId",
                table: "Regions",
                newName: "IX_Regions_AreaId");

            migrationBuilder.RenameIndex(
                name: "IX_DISTRICT_RegionId",
                table: "Districts",
                newName: "IX_Districts_RegionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Regions",
                table: "Regions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Districts",
                table: "Districts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Areas",
                table: "Areas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Districts_Regions_RegionId",
                table: "Districts",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Regions_Areas_AreaId",
                table: "Regions",
                column: "AreaId",
                principalTable: "Areas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_STORE_Districts_DistrictId",
                table: "STORE",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
