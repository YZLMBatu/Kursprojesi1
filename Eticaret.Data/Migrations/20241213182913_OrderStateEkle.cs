using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eticaret.Data.Migrations
{
    /// <inheritdoc />
    public partial class OrderStateEkle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderName",
                table: "Odemes");

            migrationBuilder.AlterColumn<string>(
                name: "OrderNo",
                table: "Odemes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "OrderState",
                table: "Odemes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "UserGuid" },
                values: new object[] { new DateTime(2024, 12, 13, 21, 29, 12, 840, DateTimeKind.Local).AddTicks(2350), new Guid("882be421-e7dd-4393-b060-ed610485f330") });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2024, 12, 13, 21, 29, 12, 843, DateTimeKind.Local).AddTicks(4315));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2024, 12, 13, 21, 29, 12, 843, DateTimeKind.Local).AddTicks(5186));

            migrationBuilder.CreateIndex(
                name: "IX_Odemes_AppUserId",
                table: "Odemes",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Odemes_AppUsers_AppUserId",
                table: "Odemes",
                column: "AppUserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Odemes_AppUsers_AppUserId",
                table: "Odemes");

            migrationBuilder.DropIndex(
                name: "IX_Odemes_AppUserId",
                table: "Odemes");

            migrationBuilder.DropColumn(
                name: "OrderState",
                table: "Odemes");

            migrationBuilder.AlterColumn<string>(
                name: "OrderNo",
                table: "Odemes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "OrderName",
                table: "Odemes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "UserGuid" },
                values: new object[] { new DateTime(2024, 12, 13, 16, 34, 8, 421, DateTimeKind.Local).AddTicks(7033), new Guid("c7d01b56-cecf-4155-bbb0-aaca3da08bff") });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2024, 12, 13, 16, 34, 8, 424, DateTimeKind.Local).AddTicks(8633));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2024, 12, 13, 16, 34, 8, 424, DateTimeKind.Local).AddTicks(9477));
        }
    }
}
