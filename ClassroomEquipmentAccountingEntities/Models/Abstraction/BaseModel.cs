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
        }

        protected BaseModel(int id, bool isHidden, DateTime rowCreatedAt, DateTime rowUpdateAt)
        {
            Id = id;
            IsHidden = isHidden;
            RowCreatedAt = rowCreatedAt;
            RowUpdateAt = rowUpdateAt;
        }

        public int Id { get; set; }
        public bool IsHidden { get; set; }
        public DateTime RowCreatedAt { get;set;}
        public DateTime RowUpdateAt { get;set;}
    }
}
