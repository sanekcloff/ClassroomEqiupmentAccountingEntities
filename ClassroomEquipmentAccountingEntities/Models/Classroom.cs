using ClassroomEquipmentAccountingEntities.Models.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassroomEquipmentAccountingEntities.Models
{
    public class Classroom : BaseModel
    {
        public Classroom()
        {
            Number = string.Empty;
            Specialization = string.Empty;
            ManagerId = 0;
            Manager = new User();
            Equipments = new HashSet<Equipment>();
        }
        public Classroom(string number, string specialization, User manager, int managerId = 0)
        {
            Number = number;
            Specialization = specialization;
            ManagerId = managerId;
            Manager = manager;
            Equipments = new HashSet<Equipment>();
        }

        public string Number { get; set; }
        public string Specialization { get; set; }
        public int ManagerId { get; set; }

        public virtual User Manager { get; set; }
        public virtual ICollection<Equipment> Equipments { get; set; }

        public bool IsEquipmentAreEmpty => Equipments.Count == 0;
        public bool IsAvailableForUse => !IsEquipmentAreEmpty;
        public override string ToString() => $"[{Id}][{Number}] {Specialization} - {Manager.ShortName} (IsHidden = {IsHidden})";
        public override bool Equals(object? obj) => obj == null || !(obj is Classroom) ? false : GetHashCode() == obj.GetHashCode();
        public override int GetHashCode() => HashCode.Combine(Number);
    }
}
