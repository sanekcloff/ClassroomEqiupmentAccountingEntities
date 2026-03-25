using ClassroomEquipmentAccountingEntities.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace ClassroomEquipmentAccountingWindowsApp.Core.Converters
{
    public class PermissionToVisibilityConverter : IValueConverter
    {
        public bool Invert { get; set; } = false;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null)
                return Visibility.Collapsed;

            var user = AppCore.Instance.CurrentUser;

            if (user == null)
                return Visibility.Collapsed;

            Permission requiredPermission;
            if (parameter is Permission permission)
            {
                requiredPermission = permission;
            }
            else if (parameter is string strPermission)
            {
                if (!Enum.TryParse(strPermission, out requiredPermission))
                {
                    return Visibility.Collapsed;
                }
            }
            else
            {
                return Visibility.Collapsed;
            }

            bool hasPermission = (user.Permissions & requiredPermission) == requiredPermission;
            var result = Invert ? !hasPermission : hasPermission;
            return result ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
