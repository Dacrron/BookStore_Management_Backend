using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bookietry.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    Book_Id = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.Book_Id);
                    table.ForeignKey(
                        name: "FK_Inventories_Books_Book_Id",
                        column: x => x.Book_Id,
                        principalTable: "Books",
                        principalColumn: "Book_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    invoice_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Book_Id = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    invoice_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    invoice_phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    invoice_mail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    invoice_address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.invoice_id);
                    table.ForeignKey(
                        name: "FK_Invoices_Books_Book_Id",
                        column: x => x.Book_Id,
                        principalTable: "Books",
                        principalColumn: "Book_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sellers",
                columns: table => new
                {
                    Seller_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Seller_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Seller_Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Seller_email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Seller_address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sellers", x => x.Seller_id);
                });

            migrationBuilder.CreateTable(
                name: "Purchases",
                columns: table => new
                {
                    purchase_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Seller_id = table.Column<int>(type: "int", nullable: true),
                    Book_Id = table.Column<int>(type: "int", nullable: true),
                    Book_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    purchase_quantity = table.Column<int>(type: "int", nullable: false),
                    purchase_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    purchase_cost = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchases", x => x.purchase_id);
                    table.ForeignKey(
                        name: "FK_Purchases_Books_Book_Id",
                        column: x => x.Book_Id,
                        principalTable: "Books",
                        principalColumn: "Book_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Purchases_Sellers_Seller_id",
                        column: x => x.Seller_id,
                        principalTable: "Sellers",
                        principalColumn: "Seller_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_Book_Id",
                table: "Invoices",
                column: "Book_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_Book_Id",
                table: "Purchases",
                column: "Book_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_Seller_id",
                table: "Purchases",
                column: "Seller_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inventories");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Purchases");

            migrationBuilder.DropTable(
                name: "Sellers");
        }
    }
}
