using ClassroomEquipmentAccountingEntities.Core.Database;
using ClassroomEquipmentAccountingEntities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassroomEquipmentAccountingWindowsApp.Core
{
    public class AppCore
    {
        private static readonly AppCore _instance = new AppCore();
        public static AppCore Instance => _instance;
        public readonly AppDbContext AppDbContext = new AppDbContext();
        public User? CurrentUser { get => field ?? throw new Exception("Пользователь не проинициализирован!"); set; }

        private AppCore() { }
    }
}
