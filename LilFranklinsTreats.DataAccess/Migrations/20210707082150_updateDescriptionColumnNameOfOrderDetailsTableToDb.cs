using Microsoft.EntityFrameworkCore.Migrations;

namespace LilFranklinsTreats.DataAccess.Migrations
{
    public partial class updateDescriptionColumnNameOfOrderDetailsTableToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_MenuItem_MenuItemId",
                table: "OrderDetails");

            migrationBuilder.RenameColumn(
                name: "Descrption",
                table: "OrderDetails",
                newName: "Description");

            migrationBuilder.AlterColumn<int>(
                name: "MenuItemId",
                table: "OrderDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_MenuItem_MenuItemId",
                table: "OrderDetails",
                column: "MenuItemId",
                principalTable: "MenuItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_MenuItem_MenuItemId",
                table: "OrderDetails");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "OrderDetails",
                newName: "Descrption");

            migrationBuilder.AlterColumn<int>(
                name: "MenuItemId",
                table: "OrderDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_MenuItem_MenuItemId",
                table: "OrderDetails",
                column: "MenuItemId",
                principalTable: "MenuItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
