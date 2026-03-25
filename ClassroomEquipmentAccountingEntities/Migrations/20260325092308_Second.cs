using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClassroomEquipmentAccountingEntities.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Users_RowAddedById",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Classrooms_Users_RowAddedById",
                table: "Classrooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Equipments_Users_RowAddedById",
                table: "Equipments");

            migrationBuilder.DropForeignKey(
                name: "FK_RepairRequestEquipment_Users_RowAddedById",
                table: "RepairRequestEquipment");

            migrationBuilder.DropForeignKey(
                name: "FK_RepairRequests_Users_RowAddedById",
                table: "RepairRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_RowAddedById",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_RowAddedById",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_RepairRequests_RowAddedById",
                table: "RepairRequests");

            migrationBuilder.DropIndex(
                name: "IX_RepairRequestEquipment_RowAddedById",
                table: "RepairRequestEquipment");

            migrationBuilder.DropIndex(
                name: "IX_Equipments_RowAddedById",
                table: "Equipments");

            migrationBuilder.DropIndex(
                name: "IX_Classrooms_RowAddedById",
                table: "Classrooms");

            migrationBuilder.DropIndex(
                name: "IX_Categories_RowAddedById",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "RowAddedById",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RowAddedById",
                table: "RepairRequests");

            migrationBuilder.DropColumn(
                name: "RowAddedById",
                table: "RepairRequestEquipment");

            migrationBuilder.DropColumn(
                name: "RowAddedById",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "RowAddedById",
                table: "Classrooms");

            migrationBuilder.DropColumn(
                name: "RowAddedById",
                table: "Categories");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RowAddedById",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RowAddedById",
                table: "RepairRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RowAddedById",
                table: "RepairRequestEquipment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RowAddedById",
                table: "Equipments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RowAddedById",
                table: "Classrooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RowAddedById",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RowAddedById",
                table: "Users",
                column: "RowAddedById");

            migrationBuilder.CreateIndex(
                name: "IX_RepairRequests_RowAddedById",
                table: "RepairRequests",
                column: "RowAddedById");

            migrationBuilder.CreateIndex(
                name: "IX_RepairRequestEquipment_RowAddedById",
                table: "RepairRequestEquipment",
                column: "RowAddedById");

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_RowAddedById",
                table: "Equipments",
                column: "RowAddedById");

            migrationBuilder.CreateIndex(
                name: "IX_Classrooms_RowAddedById",
                table: "Classrooms",
                column: "RowAddedById");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_RowAddedById",
                table: "Categories",
                column: "RowAddedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Users_RowAddedById",
                table: "Categories",
                column: "RowAddedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Classrooms_Users_RowAddedById",
                table: "Classrooms",
                column: "RowAddedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Equipments_Users_RowAddedById",
                table: "Equipments",
                column: "RowAddedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RepairRequestEquipment_Users_RowAddedById",
                table: "RepairRequestEquipment",
                column: "RowAddedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RepairRequests_Users_RowAddedById",
                table: "RepairRequests",
                column: "RowAddedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_RowAddedById",
                table: "Users",
                column: "RowAddedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
