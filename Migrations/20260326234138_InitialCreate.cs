using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eternal_api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BRAND",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    isActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BRAND", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CITY",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    State = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Country = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Prefix = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CITY", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FAMILY",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Sku = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Volume = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Unit = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    isActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FAMILY", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HIST_PRICE",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Price = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    FamilyId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HIST_PRICE", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Planograms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP AT TIME ZONE 'UTC'"),
                    isActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planograms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "STORE",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    HasPlanogram = table.Column<bool>(type: "boolean", nullable: false),
                    CityId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STORE", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PRODUCT",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    BRAND_ID = table.Column<Guid>(type: "uuid", nullable: false),
                    FAMILY_ID = table.Column<Guid>(type: "uuid", nullable: false),
                    CODE = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NAME = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IMAGE_FILE_NAME = table.Column<string>(type: "text", nullable: true),
                    IS_ACTIVE = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCT", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PRODUCT_BRAND_BRAND_ID",
                        column: x => x.BRAND_ID,
                        principalTable: "BRAND",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SALES_ROUTE",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    CityId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SALES_ROUTE", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SALES_ROUTE_CITY_CityId",
                        column: x => x.CityId,
                        principalTable: "CITY",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DISTRIBUTION",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    X_POSITION = table.Column<int>(type: "integer", nullable: false),
                    Y_POSITION = table.Column<int>(type: "integer", nullable: false),
                    PLANOGRAM_ID = table.Column<Guid>(type: "uuid", nullable: false),
                    PRODUCT_ID = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DISTRIBUTION", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DISTRIBUTION_PRODUCT_PRODUCT_ID",
                        column: x => x.PRODUCT_ID,
                        principalTable: "PRODUCT",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DISTRIBUTION_Planograms_PLANOGRAM_ID",
                        column: x => x.PLANOGRAM_ID,
                        principalTable: "Planograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "USER",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Rol = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    IdentityUserId = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false),
                    SalesRouteId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER", x => x.Id);
                    table.ForeignKey(
                        name: "FK_USER_SALES_ROUTE_SalesRouteId",
                        column: x => x.SalesRouteId,
                        principalTable: "SALES_ROUTE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ASSIGNMENT",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AssignedById = table.Column<Guid>(type: "uuid", nullable: false),
                    SalesRouteId = table.Column<Guid>(type: "uuid", nullable: false),
                    StoreId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ASSIGNMENT", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ASSIGNMENT_SALES_ROUTE_SalesRouteId",
                        column: x => x.SalesRouteId,
                        principalTable: "SALES_ROUTE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ASSIGNMENT_STORE_StoreId",
                        column: x => x.StoreId,
                        principalTable: "STORE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ASSIGNMENT_USER_AssignedById",
                        column: x => x.AssignedById,
                        principalTable: "USER",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ORDER",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PO = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    STATUS = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    SALESPERSON_ID = table.Column<Guid>(type: "uuid", nullable: false),
                    STORE_ID = table.Column<Guid>(type: "uuid", nullable: false),
                    PLANOGRAM_ID = table.Column<Guid>(type: "uuid", nullable: false),
                    SALES_ROUTE_ID = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ORDER", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ORDER_Planograms_PLANOGRAM_ID",
                        column: x => x.PLANOGRAM_ID,
                        principalTable: "Planograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ORDER_SALES_ROUTE_SALES_ROUTE_ID",
                        column: x => x.SALES_ROUTE_ID,
                        principalTable: "SALES_ROUTE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ORDER_STORE_STORE_ID",
                        column: x => x.STORE_ID,
                        principalTable: "STORE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ORDER_USER_SALESPERSON_ID",
                        column: x => x.SALESPERSON_ID,
                        principalTable: "USER",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "INVOICE",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Total = table.Column<decimal>(type: "numeric", nullable: false, defaultValue: 0m),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    POD = table.Column<string>(type: "text", nullable: true),
                    InvoiceNumber = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INVOICE", x => x.Id);
                    table.ForeignKey(
                        name: "FK_INVOICE_ORDER_OrderId",
                        column: x => x.OrderId,
                        principalTable: "ORDER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ORDER_DETAIL",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    QUANTITY = table.Column<int>(type: "integer", nullable: false),
                    ORDER_ID = table.Column<Guid>(type: "uuid", nullable: false),
                    PRODUCT_ID = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ORDER_DETAIL", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ORDER_DETAIL_ORDER_ORDER_ID",
                        column: x => x.ORDER_ID,
                        principalTable: "ORDER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ORDER_DETAIL_PRODUCT_PRODUCT_ID",
                        column: x => x.PRODUCT_ID,
                        principalTable: "PRODUCT",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "INVOICE_DETAIL",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    QUANTITY = table.Column<int>(type: "integer", nullable: false),
                    SUBTOTAL = table.Column<decimal>(type: "numeric", nullable: false),
                    INVOICE_ID = table.Column<Guid>(type: "uuid", nullable: false),
                    PRODUCT_ID = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INVOICE_DETAIL", x => x.ID);
                    table.ForeignKey(
                        name: "FK_INVOICE_DETAIL_INVOICE_INVOICE_ID",
                        column: x => x.INVOICE_ID,
                        principalTable: "INVOICE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_INVOICE_DETAIL_PRODUCT_PRODUCT_ID",
                        column: x => x.PRODUCT_ID,
                        principalTable: "PRODUCT",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ASSIGNMENT_AssignedById",
                table: "ASSIGNMENT",
                column: "AssignedById");

            migrationBuilder.CreateIndex(
                name: "IX_ASSIGNMENT_SalesRouteId",
                table: "ASSIGNMENT",
                column: "SalesRouteId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignment_StoreId_Unique",
                table: "ASSIGNMENT",
                column: "StoreId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DISTRIBUTION_PLANOGRAM_ID",
                table: "DISTRIBUTION",
                column: "PLANOGRAM_ID");

            migrationBuilder.CreateIndex(
                name: "IX_DISTRIBUTION_PRODUCT_ID",
                table: "DISTRIBUTION",
                column: "PRODUCT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_INVOICE_OrderId",
                table: "INVOICE",
                column: "OrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_INVOICE_DETAIL_INVOICE_ID",
                table: "INVOICE_DETAIL",
                column: "INVOICE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_INVOICE_DETAIL_PRODUCT_ID",
                table: "INVOICE_DETAIL",
                column: "PRODUCT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ORDER_PLANOGRAM_ID",
                table: "ORDER",
                column: "PLANOGRAM_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ORDER_SALES_ROUTE_ID",
                table: "ORDER",
                column: "SALES_ROUTE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ORDER_SALESPERSON_ID",
                table: "ORDER",
                column: "SALESPERSON_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ORDER_STORE_ID",
                table: "ORDER",
                column: "STORE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ORDER_DETAIL_ORDER_ID",
                table: "ORDER_DETAIL",
                column: "ORDER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ORDER_DETAIL_PRODUCT_ID",
                table: "ORDER_DETAIL",
                column: "PRODUCT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCT_BRAND_ID",
                table: "PRODUCT",
                column: "BRAND_ID");

            migrationBuilder.CreateIndex(
                name: "IX_SALES_ROUTE_CityId",
                table: "SALES_ROUTE",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_SALES_ROUTE_Code",
                table: "SALES_ROUTE",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_USER_IdentityUserId",
                table: "USER",
                column: "IdentityUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_USER_SalesRouteId",
                table: "USER",
                column: "SalesRouteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ASSIGNMENT");

            migrationBuilder.DropTable(
                name: "DISTRIBUTION");

            migrationBuilder.DropTable(
                name: "FAMILY");

            migrationBuilder.DropTable(
                name: "HIST_PRICE");

            migrationBuilder.DropTable(
                name: "INVOICE_DETAIL");

            migrationBuilder.DropTable(
                name: "ORDER_DETAIL");

            migrationBuilder.DropTable(
                name: "INVOICE");

            migrationBuilder.DropTable(
                name: "PRODUCT");

            migrationBuilder.DropTable(
                name: "ORDER");

            migrationBuilder.DropTable(
                name: "BRAND");

            migrationBuilder.DropTable(
                name: "Planograms");

            migrationBuilder.DropTable(
                name: "STORE");

            migrationBuilder.DropTable(
                name: "USER");

            migrationBuilder.DropTable(
                name: "SALES_ROUTE");

            migrationBuilder.DropTable(
                name: "CITY");
        }
    }
}
