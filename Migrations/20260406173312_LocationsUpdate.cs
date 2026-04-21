using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eternal_api.Migrations
{
    /// <inheritdoc />
    public partial class LocationsUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                table: "STORE",
                newName: "Street");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "STORE",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<Guid>(
                name: "DistrictId",
                table: "STORE",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "StoreNumber",
                table: "STORE",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                table: "STORE",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ZoneNumber",
                table: "STORE",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Areas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    AreaId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Regions_Areas_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Districts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    RegionId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Districts_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_STORE_CityId",
                table: "STORE",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_STORE_DistrictId",
                table: "STORE",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Districts_RegionId",
                table: "Districts",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_AreaId",
                table: "Regions",
                column: "AreaId");

            migrationBuilder.AddForeignKey(
                name: "FK_STORE_CITY_CityId",
                table: "STORE",
                column: "CityId",
                principalTable: "CITY",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_STORE_CITY_CityId",
                table: "STORE");

            migrationBuilder.DropForeignKey(
                name: "FK_STORE_Districts_DistrictId",
                table: "STORE");

            migrationBuilder.DropTable(
                name: "Districts");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropTable(
                name: "Areas");

            migrationBuilder.DropIndex(
                name: "IX_STORE_CityId",
                table: "STORE");

            migrationBuilder.DropIndex(
                name: "IX_STORE_DistrictId",
                table: "STORE");

            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "STORE");

            migrationBuilder.DropColumn(
                name: "StoreNumber",
                table: "STORE");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "STORE");

            migrationBuilder.DropColumn(
                name: "ZoneNumber",
                table: "STORE");

            migrationBuilder.RenameColumn(
                name: "Street",
                table: "STORE",
                newName: "Address");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "STORE",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}
