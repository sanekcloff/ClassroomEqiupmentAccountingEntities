using ClassroomEquipmentAccountingEntities.Core.Database;
using ClassroomEquipmentAccountingEntities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ClassroomEquipmentAccountingWindowsApp.Core
{
    public class AppCore
    {
        private static readonly AppCore _instance = new AppCore();
        public static AppCore Instance => _instance;
        public readonly AppDbContext AppDbContext = new AppDbContext();

        private User? _currentUser;
        public event EventHandler? CurrentUserChanged;

        public User? CurrentUser
        {
            get => _currentUser ?? throw new Exception("Пользователь не проинициализирован!");
            set
            {
                _currentUser = value;
                // Обновляем состояние команд в UI и оповещаем подписчиков
                CommandManager.InvalidateRequerySuggested();
                CurrentUserChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private AppCore() { }
    }
}
