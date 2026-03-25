using ClassroomEquipmentAccountingEntities.Models.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassroomEquipmentAccountingEntities.Models
{
    public class Equipment : BaseModel
    {
        public string SerialNumber { get; set; }
        public string InventoryNumber { get; set; }
        public int CategoryId { get; set; }
        public int ClassroomId { get; set; }
        public Status Status { get; set; }
        public string Model { get; set; }
        public DateTime CommissioningDate { get; set; }
        public DateTime WaranityEndDate { get; set; }

        public virtual Category Category { get; set; }
        public virtual Classroom Classroom { get; set; }

        public ushort Age
        {
            get
            {
                var age = DateTime.Now.Year - CommissioningDate.Year;
                if (DateTime.Now.AddYears(-age) < CommissioningDate) age--;
                return Convert.ToUInt16(age);
            }
        }
        public void TransferToReserve() => Status = Status.Reserve;
        public void MarkDecommission() => Status = Status.Decommissioned;
        public override bool Equals(object? obj) => obj == null || !(obj is Equipment) ? false : GetHashCode() == obj.GetHashCode();
        public override int GetHashCode() => HashCode.Combine(SerialNumber, InventoryNumber, Model);
        public override string ToString() => $"[{Id}][Serial - {SerialNumber}] [Inventory - {InventoryNumber}] {Category} - {Classroom}, Status: {Status}";

        public static IEnumerable<Equipment> operator +(Equipment equipment1, Equipment equipment2)
        {
            if (equipment1 != null) yield return equipment1;
            if (equipment2 != null) yield return equipment2;
        }
        public static IEnumerable<Equipment> operator +(IEnumerable<Equipment> collection, Equipment equipmentToAdd)
        {
            if (collection == null) yield break;
            foreach (var equipment in collection)
                yield return equipment;
            if (equipmentToAdd != null)
                yield return equipmentToAdd;
        }
        public static IEnumerable<Equipment> operator +(Equipment equipmentToAdd, IEnumerable<Equipment> collection)
        {
            if (collection == null) yield break;
            foreach (var equipment in collection)
                yield return equipment;
            if (equipmentToAdd != null)
                yield return equipmentToAdd;
        }
        public static IEnumerable<Equipment> operator -(Equipment equipment1, Equipment equipment2)
        {
            if (equipment1 == null)
                yield break;
            if (equipment2 == null || !equipment1.Equals(equipment2))
                yield return equipment1;
        }
        public static IEnumerable<Equipment> operator -(IEnumerable<Equipment> source, Equipment equipmentToRemove)
        {
            if (source == null)
                yield break;
            if (equipmentToRemove == null)
            {
                foreach (var item in source)
                    yield return item;
                yield break;
            }
            foreach (var item in source)
            {
                if (!item.Equals(equipmentToRemove))
                    yield return item;
            }
        }

    }
}
