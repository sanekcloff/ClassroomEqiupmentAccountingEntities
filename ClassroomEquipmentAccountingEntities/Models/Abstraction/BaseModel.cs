using System;
using System.Collections.Generic;
using System.Text;

namespace ClassroomEquipmentAccountingEntities.Models.Abstraction
{
    public abstract class BaseModel
    {
        protected BaseModel()
        {
            Id = 0;
            IsHidden = false;
            RowCreatedAt = DateTime.Now;
            RowUpdateAt = DateTime.Now;
            RowAddedBy = null!;
        }

        protected BaseModel(int id, bool isHidden, DateTime rowCreatedAt, DateTime rowUpdateAt, User rowAddedBy)
        {
            Id = id;
            IsHidden = isHidden;
            RowCreatedAt = rowCreatedAt;
            RowUpdateAt = rowUpdateAt;
            RowAddedBy = rowAddedBy;
        }

        public int Id { get; set; }
        public bool IsHidden { get; set; }
        public DateTime RowCreatedAt { get;set;}
        public DateTime RowUpdateAt { get;set;}
        public User RowAddedBy { get; set; }
    }
}
