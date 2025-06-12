using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantMVC.Migrations
{
    /// <inheritdoc />
    public partial class newEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LocalOrderItem_LocalOrder_OrderId",
                table: "LocalOrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_LocalOrderItem_MenuItem_MenuItemId",
                table: "LocalOrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuItem_Categories_CategoryId",
                table: "MenuItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_LocalOrder_OrderId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Table_TableId",
                table: "Reservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Table",
                table: "Table");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuItem",
                table: "MenuItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LocalOrderItem",
                table: "LocalOrderItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LocalOrder",
                table: "LocalOrder");

            migrationBuilder.RenameTable(
                name: "Table",
                newName: "Tables");

            migrationBuilder.RenameTable(
                name: "MenuItem",
                newName: "MenuItems");

            migrationBuilder.RenameTable(
                name: "LocalOrderItem",
                newName: "LocalOrderItems");

            migrationBuilder.RenameTable(
                name: "LocalOrder",
                newName: "LocalOrders");

            migrationBuilder.RenameIndex(
                name: "IX_MenuItem_CategoryId",
                table: "MenuItems",
                newName: "IX_MenuItems_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_LocalOrderItem_OrderId",
                table: "LocalOrderItems",
                newName: "IX_LocalOrderItems_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_LocalOrderItem_MenuItemId",
                table: "LocalOrderItems",
                newName: "IX_LocalOrderItems_MenuItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tables",
                table: "Tables",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuItems",
                table: "MenuItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LocalOrderItems",
                table: "LocalOrderItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LocalOrders",
                table: "LocalOrders",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "OnlineOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnlineOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OnlineOrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    MenuItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnlineOrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OnlineOrderItems_MenuItems_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OnlineOrderItems_OnlineOrders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "OnlineOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OnlineOrderItems_MenuItemId",
                table: "OnlineOrderItems",
                column: "MenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OnlineOrderItems_OrderId",
                table: "OnlineOrderItems",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_LocalOrderItems_LocalOrders_OrderId",
                table: "LocalOrderItems",
                column: "OrderId",
                principalTable: "LocalOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LocalOrderItems_MenuItems_MenuItemId",
                table: "LocalOrderItems",
                column: "MenuItemId",
                principalTable: "MenuItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItems_Categories_CategoryId",
                table: "MenuItems",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_LocalOrders_OrderId",
                table: "Reservations",
                column: "OrderId",
                principalTable: "LocalOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Tables_TableId",
                table: "Reservations",
                column: "TableId",
                principalTable: "Tables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LocalOrderItems_LocalOrders_OrderId",
                table: "LocalOrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_LocalOrderItems_MenuItems_MenuItemId",
                table: "LocalOrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_Categories_CategoryId",
                table: "MenuItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_LocalOrders_OrderId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Tables_TableId",
                table: "Reservations");

            migrationBuilder.DropTable(
                name: "OnlineOrderItems");

            migrationBuilder.DropTable(
                name: "OnlineOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tables",
                table: "Tables");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuItems",
                table: "MenuItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LocalOrders",
                table: "LocalOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LocalOrderItems",
                table: "LocalOrderItems");

            migrationBuilder.RenameTable(
                name: "Tables",
                newName: "Table");

            migrationBuilder.RenameTable(
                name: "MenuItems",
                newName: "MenuItem");

            migrationBuilder.RenameTable(
                name: "LocalOrders",
                newName: "LocalOrder");

            migrationBuilder.RenameTable(
                name: "LocalOrderItems",
                newName: "LocalOrderItem");

            migrationBuilder.RenameIndex(
                name: "IX_MenuItems_CategoryId",
                table: "MenuItem",
                newName: "IX_MenuItem_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_LocalOrderItems_OrderId",
                table: "LocalOrderItem",
                newName: "IX_LocalOrderItem_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_LocalOrderItems_MenuItemId",
                table: "LocalOrderItem",
                newName: "IX_LocalOrderItem_MenuItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Table",
                table: "Table",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuItem",
                table: "MenuItem",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LocalOrder",
                table: "LocalOrder",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LocalOrderItem",
                table: "LocalOrderItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LocalOrderItem_LocalOrder_OrderId",
                table: "LocalOrderItem",
                column: "OrderId",
                principalTable: "LocalOrder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LocalOrderItem_MenuItem_MenuItemId",
                table: "LocalOrderItem",
                column: "MenuItemId",
                principalTable: "MenuItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItem_Categories_CategoryId",
                table: "MenuItem",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_LocalOrder_OrderId",
                table: "Reservations",
                column: "OrderId",
                principalTable: "LocalOrder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Table_TableId",
                table: "Reservations",
                column: "TableId",
                principalTable: "Table",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
