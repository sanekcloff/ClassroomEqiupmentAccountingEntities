using ClassroomEquipmentAccountingEntities.Models.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassroomEquipmentAccountingEntities.Models
{
    public class RepairRequestEquipment : BaseModel
    {
        public RepairRequestEquipment()
        {
            RepairRequest = new();
            Equipment = new();
        }
        public RepairRequestEquipment(RepairRequest repairRequest, Equipment equipment)
        {
            RepairRequest = repairRequest;
            Equipment = equipment;
        }

        public RepairRequest RepairRequest { get; set; }
        public Equipment Equipment { get; set; }
        public override string ToString() => $"{RepairRequest} - {Equipment}";
        public override bool Equals(object? obj) => obj == null || !(obj is RepairRequestEquipment) ? false : true;
        public override int GetHashCode() => HashCode.Combine(RepairRequest,Equipment);
    }
}
