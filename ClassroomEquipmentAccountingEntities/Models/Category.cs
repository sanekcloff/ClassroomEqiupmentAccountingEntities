using ClassroomEquipmentAccountingEntities.Models.Abstraction;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ClassroomEquipmentAccountingEntities.Models
{
    public class Category : BaseModel
    {
        public Category()
        {
            Title = string.Empty;
            Description = string.Empty;
            Equipments = new HashSet<Equipment>();
        }
        public Category(string title, string? description)
        {
            Title = title;
            Description = description;
            Equipments = new HashSet<Equipment>();
        }

        public string Title { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<Equipment> Equipments { get; set; }
        public override bool Equals(object? obj) => obj == null || !(obj is Category) ? false : true;
        public override int GetHashCode() => HashCode.Combine(Title);
        public override string ToString() => $"[{Id}][{GetType().Name}] {Title}: {Description} (IsHidden = {IsHidden})";
    }
}
