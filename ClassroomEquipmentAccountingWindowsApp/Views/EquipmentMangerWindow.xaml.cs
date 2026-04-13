using ClassroomEquipmentAccountingEntities.Models;
using ClassroomEquipmentAccountingWindowsApp.ViewModels;
using System.Windows;

namespace ClassroomEquipmentAccountingWindowsApp.Views
{
    public partial class EquipmentMangerWindow : Window
    {
        public EquipmentMangerWindow()
        {
            InitializeComponent();
            DataContext = new EquipmentMangerWindowViewModel(null);
        }

        public EquipmentMangerWindow(Equipment equipment)
        {
            InitializeComponent();
            DataContext = new EquipmentMangerWindowViewModel(equipment);
        }
    }
}
