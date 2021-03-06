﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MaiDan.Accounting.Dal.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountLog",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(nullable: false),
                    RequestId = table.Column<Guid>(nullable: false),
                    RequestPath = table.Column<string>(nullable: true),
                    RequestMethod = table.Column<string>(nullable: true),
                    RequestBody = table.Column<string>(nullable: true),
                    ObjectType = table.Column<string>(nullable: true),
                    ActionType = table.Column<string>(nullable: true),
                    OldValue = table.Column<string>(nullable: true),
                    NewValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Day",
                columns: table => new
                {
                    Date = table.Column<DateTime>(nullable: false),
                    Closed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Day", x => x.Date);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethod",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethod", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Slip",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PaymentDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slip", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DaySlip",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DayDate = table.Column<DateTime>(nullable: true),
                    ClosingDate = table.Column<DateTime>(nullable: false),
                    CashAmount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DaySlip", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DaySlip_Day_DayDate",
                        column: x => x.DayDate,
                        principalTable: "Day",
                        principalColumn: "Date",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SlipPayment",
                columns: table => new
                {
                    SlipId = table.Column<int>(nullable: false),
                    Index = table.Column<int>(nullable: false),
                    PaymentMethodId = table.Column<string>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SlipPayment", x => new { x.SlipId, x.Index });
                    table.ForeignKey(
                        name: "FK_SlipPayment_PaymentMethod_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SlipPayment_Slip_SlipId",
                        column: x => x.SlipId,
                        principalTable: "Slip",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DaySlip_DayDate",
                table: "DaySlip",
                column: "DayDate");

            migrationBuilder.CreateIndex(
                name: "IX_SlipPayment_PaymentMethodId",
                table: "SlipPayment",
                column: "PaymentMethodId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountLog");

            migrationBuilder.DropTable(
                name: "DaySlip");

            migrationBuilder.DropTable(
                name: "SlipPayment");

            migrationBuilder.DropTable(
                name: "Day");

            migrationBuilder.DropTable(
                name: "PaymentMethod");

            migrationBuilder.DropTable(
                name: "Slip");
        }
    }
}
