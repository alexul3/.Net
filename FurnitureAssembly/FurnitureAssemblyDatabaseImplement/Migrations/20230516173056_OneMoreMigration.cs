using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FurnitureAssemblyDatabaseImplement.Migrations
{
    /// <inheritdoc />
    public partial class OneMoreMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderInfoId",
                table: "Orders",
                column: "OrderInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderInfos_OrderInfoId",
                table: "Orders",
                column: "OrderInfoId",
                principalTable: "OrderInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderInfos_OrderInfoId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderInfoId",
                table: "Orders");
        }
    }
}
