using ClassroomEquipmentAccountingEntities.Core.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassroomEquipmentAccountingEntities.Core
{
    public class AppInstance
    {
        public readonly AppDbContext AppDbContextInstance = new AppDbContext();
    }
}
