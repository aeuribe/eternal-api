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
                name: "CITY",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    State = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Country = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CITY", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HIST_PRICE",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Price = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false)
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
                    isActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planograms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PODs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PODs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PRODUCT",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Category = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    SKU = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    isActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCT", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "STORE",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    CityId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STORE", x => x.Id);
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
                    CityId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER", x => x.Id);
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DISTRIBUTION_Planograms_PLANOGRAM_ID",
                        column: x => x.PLANOGRAM_ID,
                        principalTable: "Planograms",
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
                    STATUS = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    SALESPERSON_ID = table.Column<Guid>(type: "uuid", nullable: false),
                    STORE_ID = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ORDER", x => x.ID);
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
                name: "visit_logs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    VisitDate = table.Column<DateOnly>(type: "date", nullable: false),
                    SalespersonId = table.Column<Guid>(type: "uuid", nullable: false),
                    StoreId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_visit_logs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_visit_logs_STORE_StoreId",
                        column: x => x.StoreId,
                        principalTable: "STORE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_visit_logs_USER_SalespersonId",
                        column: x => x.SalespersonId,
                        principalTable: "USER",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "INVOICE",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Total = table.Column<decimal>(type: "numeric", nullable: false, defaultValue: 0m),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    PodId = table.Column<Guid>(type: "uuid", nullable: true)
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
                    table.ForeignKey(
                        name: "FK_INVOICE_PODs_PodId",
                        column: x => x.PodId,
                        principalTable: "PODs",
                        principalColumn: "Id");
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "INVOICE_DETAIL",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    QUANTITY = table.Column<int>(type: "integer", nullable: false),
                    SUBTOTAL = table.Column<float>(type: "real", nullable: false),
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "IX_INVOICE_PodId",
                table: "INVOICE",
                column: "PodId",
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
                name: "IX_visit_logs_SalespersonId",
                table: "visit_logs",
                column: "SalespersonId");

            migrationBuilder.CreateIndex(
                name: "IX_visit_logs_StoreId",
                table: "visit_logs",
                column: "StoreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CITY");

            migrationBuilder.DropTable(
                name: "DISTRIBUTION");

            migrationBuilder.DropTable(
                name: "HIST_PRICE");

            migrationBuilder.DropTable(
                name: "INVOICE_DETAIL");

            migrationBuilder.DropTable(
                name: "ORDER_DETAIL");

            migrationBuilder.DropTable(
                name: "visit_logs");

            migrationBuilder.DropTable(
                name: "Planograms");

            migrationBuilder.DropTable(
                name: "INVOICE");

            migrationBuilder.DropTable(
                name: "PRODUCT");

            migrationBuilder.DropTable(
                name: "ORDER");

            migrationBuilder.DropTable(
                name: "PODs");

            migrationBuilder.DropTable(
                name: "STORE");

            migrationBuilder.DropTable(
                name: "USER");
        }
    }
}
