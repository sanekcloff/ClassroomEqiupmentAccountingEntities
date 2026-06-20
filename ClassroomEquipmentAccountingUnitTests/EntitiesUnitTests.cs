using ClassroomEquipmentAccountingEntities.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace ClassroomEquipmentAccountingUnitTests
{
    public class EntitiesUnitTests
    {
        [Fact]
        public void RepairRequest_DefaultConstructor_ShouldInitializeProperties()
        {
            // Arrange & Act
            var repairRequest = new RepairRequest();

            // Assert
            Assert.Equal(DateTime.Now.Date, repairRequest.StartDate.Date);
            Assert.Null(repairRequest.EndDate);
            Assert.Equal("Описание отсутвует", repairRequest.Description);
            Assert.NotNull(repairRequest.RepairRequestEquipments);
            Assert.Empty(repairRequest.RepairRequestEquipments);
        }

        [Fact]
        public void RepairRequest_CustomConstructor_ShouldSetProperties()
        {
            // Arrange
            var startDate = new DateTime(2023, 1, 1);
            var endDate = new DateTime(2023, 1, 10);
            var description = "Test description";

            // Act
            var repairRequest = new RepairRequest(startDate, endDate, description);

            // Assert
            Assert.Equal(startDate, repairRequest.StartDate);
            Assert.Equal(endDate, repairRequest.EndDate);
            Assert.Equal(description, repairRequest.Description);
            Assert.NotNull(repairRequest.RepairRequestEquipments);
        }

        [Fact]
        public void RepairRequest_AddEquipment_ShouldAddSingleEquipment()
        {
            // Arrange
            var repairRequest = new RepairRequest();
            var equipment = new Equipment { Id = 1, Model = "Test Equipment" };

            // Act
            repairRequest.AddEquipment(equipment);

            // Assert
            Assert.Single(repairRequest.RepairRequestEquipments);
            Assert.Contains(repairRequest.RepairRequestEquipments, e => e.Equipment == equipment);
        }

        [Fact]
        public void RepairRequest_AddEquipment_ShouldAddMultipleEquipments()
        {
            // Arrange
            var repairRequest = new RepairRequest();
            var equipmentList = new List<Equipment>
            {
                new Equipment { Id = 1, Model = "Equipment 1" },
                new Equipment { Id = 2, Model = "Equipment 2" }
            };

            // Act
            repairRequest.AddEquipment(equipmentList);

            // Assert
            Assert.Equal(2, repairRequest.RepairRequestEquipments.Count);
            foreach (var equipment in equipmentList)
            {
                Assert.Contains(repairRequest.RepairRequestEquipments, e => e.Equipment == equipment);
            }
        }

        [Fact]
        public void RepairRequest_BeetweenDays_ShouldReturnCorrectValue()
        {
            // Arrange
            var startDate = new DateTime(2023, 1, 1);
            var endDate = new DateTime(2023, 1, 10);
            var repairRequest = new RepairRequest(startDate, endDate, "Test description");

            // Act
            var betweenDays = repairRequest.BeetweenDays;

            // Assert
            Assert.Equal(9, betweenDays);
        }

        [Fact]
        public void RepairRequest_BeetweenDays_ShouldReturnZeroIfEndDateIsNull()
        {
            // Arrange
            var repairRequest = new RepairRequest
            {
                StartDate = new DateTime(2023, 1, 1),
                EndDate = null
            };

            // Act
            var betweenDays = repairRequest.BeetweenDays;

            // Assert
            Assert.Equal(0, betweenDays);
        }
    }
}
