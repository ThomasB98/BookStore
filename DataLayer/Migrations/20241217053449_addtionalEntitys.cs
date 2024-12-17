using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class addtionalEntitys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Users_UserId",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Order",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_UserId",
                table: "Order",
                newName: "IX_Order_userId");

            migrationBuilder.AlterColumn<float>(
                name: "price",
                table: "OrderItem",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "OrderItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<float>(
                name: "totalAmount",
                table: "Order",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.CreateTable(
                name: "shippingAddress",
                columns: table => new
                {
                    ShippingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressLine1 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AddressLine2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    State = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shippingAddress", x => x.ShippingId);
                    table.ForeignKey(
                        name: "FK_shippingAddress_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_shippingAddress_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_BookId",
                table: "OrderItem",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_shippingAddress_OrderId",
                table: "shippingAddress",
                column: "OrderId",
                unique: true,
                filter: "[OrderId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_shippingAddress_UserId",
                table: "shippingAddress",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Users_userId",
                table: "Order",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Book_BookId",
                table: "OrderItem",
                column: "BookId",
                principalTable: "Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Users_userId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Book_BookId",
                table: "OrderItem");

            migrationBuilder.DropTable(
                name: "shippingAddress");

            migrationBuilder.DropIndex(
                name: "IX_OrderItem_BookId",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "OrderItem");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Order",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_userId",
                table: "Order",
                newName: "IX_Order_UserId");

            migrationBuilder.AlterColumn<decimal>(
                name: "price",
                table: "OrderItem",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<decimal>(
                name: "totalAmount",
                table: "Order",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Users_UserId",
                table: "Order",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
