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
            if (value is not User user)
                return Visibility.Collapsed;

            if (parameter is not Permission requiredPermission)
                return Visibility.Collapsed;

            bool hasPermission;

            if (requiredPermission == Permission.None)
            {
                hasPermission = user.Permissions == Permission.None;
            }
            else
            {
                hasPermission = (user.Permissions & requiredPermission) == requiredPermission;
            }
                return (Invert ? !hasPermission : hasPermission) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
