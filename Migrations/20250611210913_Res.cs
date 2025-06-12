using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantMVC.Migrations
{
    /// <inheritdoc />
    public partial class Res : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_LocalOrders_OrderId",
                table: "Reservations");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "Reservations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_LocalOrders_OrderId",
                table: "Reservations",
                column: "OrderId",
                principalTable: "LocalOrders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_LocalOrders_OrderId",
                table: "Reservations");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_LocalOrders_OrderId",
                table: "Reservations",
                column: "OrderId",
                principalTable: "LocalOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
