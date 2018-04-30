using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MaiDan.Billing.Dal.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bill",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Total = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bill", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BillDish",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillDish", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaxRate",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    TaxId = table.Column<string>(nullable: false),
                    ValidityStartDate = table.Column<DateTime>(nullable: false),
                    ValidityEndDate = table.Column<DateTime>(nullable: false),
                    Rate = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxRate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DishPrice",
                columns: table => new
                {
                    DishId = table.Column<string>(nullable: false),
                    ValidityStartDate = table.Column<DateTime>(nullable: false),
                    ValidityEndDate = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DishPrice", x => new { x.DishId, x.ValidityStartDate });
                    table.ForeignKey(
                        name: "FK_DishPrice_BillDish_DishId",
                        column: x => x.DishId,
                        principalTable: "BillDish",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BillLine",
                columns: table => new
                {
                    BillId = table.Column<int>(nullable: false),
                    Index = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    TaxRateId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillLine", x => new { x.BillId, x.Index });
                    table.ForeignKey(
                        name: "FK_BillLine_Bill_BillId",
                        column: x => x.BillId,
                        principalTable: "Bill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BillLine_TaxRate_TaxRateId",
                        column: x => x.TaxRateId,
                        principalTable: "TaxRate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BillTax",
                columns: table => new
                {
                    BillId = table.Column<int>(nullable: false),
                    Index = table.Column<int>(nullable: false),
                    TaxRateId = table.Column<string>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillTax", x => new { x.BillId, x.Index });
                    table.ForeignKey(
                        name: "FK_BillTax_Bill_BillId",
                        column: x => x.BillId,
                        principalTable: "Bill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BillTax_TaxRate_TaxRateId",
                        column: x => x.TaxRateId,
                        principalTable: "TaxRate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillLine_TaxRateId",
                table: "BillLine",
                column: "TaxRateId");

            migrationBuilder.CreateIndex(
                name: "IX_BillTax_TaxRateId",
                table: "BillTax",
                column: "TaxRateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillLine");

            migrationBuilder.DropTable(
                name: "BillTax");

            migrationBuilder.DropTable(
                name: "DishPrice");

            migrationBuilder.DropTable(
                name: "Bill");

            migrationBuilder.DropTable(
                name: "TaxRate");

            migrationBuilder.DropTable(
                name: "BillDish");
        }
    }
}
