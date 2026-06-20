using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ClassroomEquipmentAccountingEntities.Migrations
{
    /// <inheritdoc />
    public partial class Init_data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "IsHidden", "RowCreatedAt", "RowUpdateAt", "Title" },
                values: new object[,]
                {
                    { 1, "Компьютерное оборудование", false, new DateTime(2000, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2000, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Компьютеры" },
                    { 2, "Печатающие устройства", false, new DateTime(2000, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2000, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Принтеры" },
                    { 3, "Проекционное оборудование", false, new DateTime(2000, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2000, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Проекторы" },
                    { 4, "Сетевое оборудование", false, new DateTime(2000, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2000, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Сеть" },
                    { 5, "Кабинетная мебель", false, new DateTime(2000, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2000, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Мебель" }
                });

            migrationBuilder.InsertData(
                table: "Classrooms",
                columns: new[] { "Id", "IsHidden", "ManagerId", "Number", "RowCreatedAt", "RowUpdateAt", "Specialization" },
                values: new object[,]
                {
                    { 1, false, 1, "101", new DateTime(2000, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2000, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Компьютерный кабинет" },
                    { 2, false, 1, "102", new DateTime(2000, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2000, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Лаборатория сетей" },
                    { 3, false, 1, "201", new DateTime(2000, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2000, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Кабинет программирования" },
                    { 4, false, 1, "202", new DateTime(2000, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2000, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Мультимедийный кабинет" },
                    { 5, false, 1, "301", new DateTime(2000, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2000, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Резервный кабинет" }
                });

            migrationBuilder.InsertData(
                table: "RepairRequests",
                columns: new[] { "Id", "Description", "EndDate", "IsHidden", "RowCreatedAt", "RowUpdateAt", "StartDate" },
                values: new object[,]
                {
                    { 1, "Не включается компьютер", null, false, new DateTime(2000, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2000, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Заменить картридж принтера", new DateTime(2026, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2000, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2000, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "Проектор не переключает вход", null, false, new DateTime(2000, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2000, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "Проблемы в сети, пакетные потери", new DateTime(2026, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2000, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2000, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "Поломка стола", null, false, new DateTime(2000, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2000, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "LastName", "PasswordHash" },
                values: new object[] { "Быстров", "500:Nj5sBszKuiPv9Syy3eV4sg==:D0Z5twb40MEuBiqfdHZbRKVWgZwK4PAeR3XB/VCAjlM=" });

            migrationBuilder.InsertData(
                table: "Equipments",
                columns: new[] { "Id", "CategoryId", "ClassroomId", "CommissioningDate", "InventoryNumber", "IsHidden", "Model", "RowCreatedAt", "RowUpdateAt", "SerialNumber", "Status", "WaranityEndDate" },
                values: new object[,]
                {
                    { 1, 1, 1, new DateTime(2020, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "INV-0001", false, "Dell OptiPlex 3080", new DateTime(2000, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2000, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "SN-CPU-0001", 0, new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, 1, new DateTime(2019, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "INV-0002", false, "HP LaserJet Pro", new DateTime(2000, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2000, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "SN-PRT-0002", 0, new DateTime(2022, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 3, 4, new DateTime(2021, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "INV-0003", false, "Epson EB-X05", new DateTime(2000, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2000, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "SN-PJT-0003", 0, new DateTime(2024, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 4, 2, new DateTime(2018, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "INV-0004", false, "Cisco Switch 2960", new DateTime(2000, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2000, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "SN-NET-0004", 0, new DateTime(2021, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 5, 5, new DateTime(2017, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "INV-0005", false, "Учебный стол Model-A", new DateTime(2000, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2000, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "SN-FUR-0005", 0, new DateTime(2020, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "RepairRequestEquipment",
                columns: new[] { "Id", "EquipmentId", "IsHidden", "RepairRequestId", "RowCreatedAt", "RowUpdateAt" },
                values: new object[,]
                {
                    { 1, 1, false, 1, new DateTime(2000, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2000, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, false, 2, new DateTime(2000, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2000, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 3, false, 3, new DateTime(2000, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2000, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 4, false, 4, new DateTime(2000, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2000, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 5, false, 5, new DateTime(2000, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2000, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "RepairRequestEquipment",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RepairRequestEquipment",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RepairRequestEquipment",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "RepairRequestEquipment",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "RepairRequestEquipment",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Equipments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Equipments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Equipments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Equipments",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Equipments",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "RepairRequests",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RepairRequests",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RepairRequests",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "RepairRequests",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "RepairRequests",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Classrooms",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "LastName", "PasswordHash" },
                values: new object[] { "Папич", "password" });
        }
    }
}
