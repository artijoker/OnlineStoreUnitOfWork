using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HttpApiServer.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CartId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Login = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    AccountId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CategoryId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    UrlImage = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProductId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    CartId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItem_Cart_CartId",
                        column: x => x.CartId,
                        principalTable: "Cart",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItem_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("25f53408-1920-46b2-ab1e-0528bd0982d3"), "Игровые консоли" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("609dd6fb-c2d1-45ad-8050-27170b0ac7d7"), "Наушники" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("879f10dc-ce53-4e63-a306-88b8681898fb"), "Телевизоры" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("af0b1a81-c3ab-42a7-b34c-1c9aa3a1029d"), "USB накопители" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("ca761852-7dc7-40aa-aaa2-d0df861800e2"), "Смарфоны" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("ea87ecbe-c441-45ce-9fac-437bc57a7251"), "Ноутбуки" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Name", "Price", "Quantity", "UrlImage" },
                values: new object[] { new Guid("5e105978-2e29-4e1b-aeec-fca3d6027ec5"), new Guid("609dd6fb-c2d1-45ad-8050-27170b0ac7d7"), "Наушники Sony WH-CH56030NW", 130.60m, 25, "https://cdn1.ozone.ru/s3/multimedia-r/wc1200/6179635779.jpg" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Name", "Price", "Quantity", "UrlImage" },
                values: new object[] { new Guid("6e9052d4-f23d-406e-a439-525424573974"), new Guid("af0b1a81-c3ab-42a7-b34c-1c9aa3a1029d"), "USB накопитель Samsung", 30m, 35, "https://cdn1.ozone.ru/multimedia/wc1200/1026248251.jpg" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Name", "Price", "Quantity", "UrlImage" },
                values: new object[] { new Guid("6f51aa7a-d936-4549-a708-d4f624516376"), new Guid("879f10dc-ce53-4e63-a306-88b8681898fb"), "Телевизор Sony KD50X81J 50", 1000.89m, 15, "https://cdn1.ozone.ru/s3/multimedia-n/wc1200/6068732087.jpg" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Name", "Price", "Quantity", "UrlImage" },
                values: new object[] { new Guid("886e8f86-3829-45b1-af91-8fd626e4e80b"), new Guid("ca761852-7dc7-40aa-aaa2-d0df861800e2"), "Смартфон Google Pixel 5a", 560m, 20, "https://cdn1.ozone.ru/s3/multimedia-3/wc250/6237328203.jpg" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Name", "Price", "Quantity", "UrlImage" },
                values: new object[] { new Guid("89596c2a-ef81-4ce5-a096-b7a49472cce4"), new Guid("25f53408-1920-46b2-ab1e-0528bd0982d3"), "Microsoft Xbox Series X", 985m, 2, "https://cdn1.ozone.ru/s3/multimedia-l/wc1200/6232471137.jpg" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Name", "Price", "Quantity", "UrlImage" },
                values: new object[] { new Guid("f2bb411f-be16-4cb4-b47e-d1008a9f12b1"), new Guid("ea87ecbe-c441-45ce-9fac-437bc57a7251"), "Ноутбук Lenovo IdeaPad 4 15IX5P6", 830.50m, 15, "https://cdn1.ozone.ru/s3/multimedia-7/wc1200/6166994971.jpg" });

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_CartId",
                table: "CartItem",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_ProductId",
                table: "CartItem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "CartItem");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
