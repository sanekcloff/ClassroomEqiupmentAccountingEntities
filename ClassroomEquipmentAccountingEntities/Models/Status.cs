using System;
using System.Collections.Generic;
using System.Text;

namespace ClassroomEquipmentAccountingEntities.Models
{
    public enum Status
    {
        Expoitation, // В эксплуатации
        Repair, // В ремонте
        Reserve, // В резерве
        Decommissioned // Снят с эксплуатации
    }
}
