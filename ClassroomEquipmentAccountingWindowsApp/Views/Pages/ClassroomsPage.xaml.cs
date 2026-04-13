using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClassroomEquipmentAccountingWindowsApp.ViewModels;

namespace ClassroomEquipmentAccountingWindowsApp.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для UsersPage.xaml
    /// </summary>
    public partial class ClassroomsPage : Page
    {
        public ClassroomsPage()
        {
            InitializeComponent();
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is ClassroomsPageViewModel vm && vm.DoubleClickCommand.CanExecute(null))
                vm.DoubleClickCommand.Execute(null);
        }
    }
}
