using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ClassroomEquipmentAccountingWindowsApp.ViewModels;
using ClassroomEquipmentAccountingEntities.Models;

namespace ClassroomEquipmentAccountingWindowsApp.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для UsersPage.xaml
    /// </summary>
    public partial class RepairRequestsPage : Page
    {
        public RepairRequestsPage()
        {
            InitializeComponent();
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is not RepairRequestsPageViewModel vm) return;

            // Получаем объект напрямую из DataContext элемента, чтобы не зависеть от SelectedItem
            if (sender is FrameworkElement fe && fe.DataContext is RepairRequest item)
            {
                if (vm.DoubleClickCommand.CanExecute(item))
                    vm.DoubleClickCommand.Execute(item);
            }
            else
            {
                // fallback
                if (vm.DoubleClickCommand.CanExecute(null))
                    vm.DoubleClickCommand.Execute(null);
            }
        }
    }
}
