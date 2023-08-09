using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.PaymentTransactions.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Counts",
                columns: table => new
                {
                    countId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    currency = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<long>(type: "bigint", nullable: false),
                    allowPartial = table.Column<bool>(type: "bit", nullable: false),
                    suscribe = table.Column<bool>(type: "bit", nullable: false),
                    payerId = table.Column<long>(type: "bigint", nullable: false),
                    paid = table.Column<bool>(type : "bit", nullable : false)

                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Counts", x => x.countId);
                });

            migrationBuilder.CreateTable(
                name: "Fields",
                columns: table => new
                {
                    fieldId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    keyWord = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<long>(type: "nvarchar(max)", nullable: false),
                    displayON = table.Column<bool>(type: "bit", nullable: false),
                    fieldsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fields", x => x.fieldId);
                });

            migrationBuilder.CreateTable(
                name: "ListOfFields",
                columns: table => new
                {
                    FieldsId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListOfFields", x => x.FieldsId);
                });

            migrationBuilder.CreateTable(
                name: "Mounts",
                columns: table => new
                {
                    MountId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    toTotal = table.Column<long>(type: "bigint", nullable: false),
                    toCurrency = table.Column<int>(type: "int", nullable: false),
                    fromTotal = table.Column<long>(type: "bigint", nullable: false),
                    fromCurrency = table.Column<int>(type: "int", nullable: false),
                    countId = table.Column<long>(type : "bigint", nullable: false),
                    factor = table.Column<int>(type : "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mounts", x => x.MountId);
                }); ;

            migrationBuilder.CreateTable(
                name: "Payers",
                columns: table => new
                {
                    PayerId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    documentType = table.Column<int>(type: "int", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable : false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    number = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payers", x => x.PayerId);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    paymentId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mountId = table.Column<long>(type: "bigint", nullable: false),
                    statusId = table.Column<long>(type: "bigint", nullable: false),
                    countId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.paymentId);
                });

            migrationBuilder.CreateTable(
                name: "Receipts",
                columns: table => new
                {
                    ReceiptId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    franchise = table.Column<int>(type: "int", nullable: false),
                    reference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    issuerName = table.Column<int>(type: "int", nullable: false),
                    authorization = table.Column<long>(type: "bigint", nullable: false),
                    paymentMethod = table.Column<int>(type: "int", nullable: false),
                    payerId = table.Column<long>(type: "bigint", nullable: false),
                    fieldsId = table.Column<long>(type: "bigint", nullable: false),
                    paymentId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receipts", x => x.ReceiptId);
                });

            migrationBuilder.CreateTable(
                name: "requesByCounts",
                columns: table => new
                {
                    RequesByCountId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    reference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountId = table.Column<long>(type: "bigint", nullable: false),
                    requestId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_requesByCounts", x => x.RequesByCountId);
                });

            migrationBuilder.CreateTable(
                name: "statuses",
                columns: table => new
                {
                    statusId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    status = table.Column<int>(type: "int", nullable: false),
                    reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_statuses", x => x.statusId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Counts");

            migrationBuilder.DropTable(
                name: "Fields");

            migrationBuilder.DropTable(
                name: "ListOfFields");

            migrationBuilder.DropTable(
                name: "Mounts");

            migrationBuilder.DropTable(
                name: "Payers");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Receipts");

            migrationBuilder.DropTable(
                name: "requesByCounts");

            migrationBuilder.DropTable(
                name: "statuses");
        }
    }
}