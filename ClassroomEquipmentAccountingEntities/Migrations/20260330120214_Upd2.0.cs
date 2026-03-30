using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClassroomEquipmentAccountingEntities.Migrations
{
    /// <inheritdoc />
    public partial class Upd20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "IsHidden", "LastName", "Login", "MiddleName", "PasswordHash", "Permissions", "RowCreatedAt", "RowUpdateAt", "Tag" },
                values: new object[] { 1, "Виталий", false, "Папич", "login", "Петрович", "password", 2097151, new DateTime(2000, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2000, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
