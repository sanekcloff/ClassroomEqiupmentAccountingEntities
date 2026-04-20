using ClassroomEquipmentAccountingEntities.Models.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClassroomEquipmentAccountingEntities.Models
{
    public class RepairRequest : BaseModel
    {
        public RepairRequest()
        {
            StartDate = DateTime.Now;
            EndDate = null;
            Description = string.Empty;
            RepairRequestEquipments = new HashSet<RepairRequestEquipment>();
        }
        public RepairRequest(DateTime startDate, DateTime endDate, string? description)
        {
            StartDate = startDate;
            EndDate = endDate;
            Description = description;
            RepairRequestEquipments = new HashSet<RepairRequestEquipment>();
        }
        private DateTime _startDate;
        private DateTime? _endDate;
        private string? _description;

        public DateTime StartDate { get => _startDate; set => _startDate = value; }
        public DateTime? EndDate { get => _endDate; set => _endDate = value; }
        public string? Description { get => string.IsNullOrWhiteSpace(_description) ? "Описание отсутвует" : _description; set => _description = value; }

        public virtual ICollection<RepairRequestEquipment> RepairRequestEquipments { get; set; }

        public ushort BeetweenDays
        {
            get
            {
                if (EndDate == null)
                    return 0; // Если EndDate не задана, возвращаем 0
                return (ushort)Math.Clamp((EndDate - StartDate).Value.Days, ushort.MinValue, ushort.MaxValue);
            }
        }
        public override string ToString() => $"[{Id}] {StartDate} - {EndDate} / Описание: {_description}";
        public override bool Equals(object? obj) => obj == null || !(obj is RepairRequest) ? false : GetHashCode() == obj.GetHashCode();
        public override int GetHashCode() => HashCode.Combine(StartDate,EndDate,Description);
        public RepairRequest AddEquipment(Equipment equipment)
        {
            if (equipment == null)
                return this;
            var repairRequestEquipment = new RepairRequestEquipment(this,equipment);
            RepairRequestEquipments.Add(repairRequestEquipment);
            return this;
        }
        public RepairRequest AddEquipment(IEnumerable<Equipment> equipments)
        {
            if (equipments == null || !equipments.Any())
                return this;
            foreach (var equipment in equipments)
            {
                RepairRequestEquipments.Add(new RepairRequestEquipment(this,equipment));
            }
            return this;
        }
    }
}
