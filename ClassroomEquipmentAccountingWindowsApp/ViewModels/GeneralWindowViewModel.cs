using ClassroomEquipmentAccountingEntities.Models;
using ClassroomEquipmentAccountingWindowsApp.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassroomEquipmentAccountingWindowsApp.ViewModels
{
    public class GeneralWindowViewModel : ViewModelBase
    {
        public GeneralWindowViewModel()
        {
            CurrentUser = AppCore.Instance.CurrentUser!; 
        }
        public User CurrentUser { get; init; }

        public bool IsCurrentUserPermissionNone => (CurrentUser.Permissions & Permission.None) != 0;
    }
}
