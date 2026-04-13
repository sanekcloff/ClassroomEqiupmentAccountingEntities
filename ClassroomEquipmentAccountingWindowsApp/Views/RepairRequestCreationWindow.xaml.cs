using ClassroomEquipmentAccountingEntities.Models;
using ClassroomEquipmentAccountingWindowsApp.ViewModels;
using System.Windows;

namespace ClassroomEquipmentAccountingWindowsApp.Views
{
    public partial class RepairRequestCreationWindow : Window
    {
        public RepairRequestCreationWindow()
        {
            InitializeComponent();
            DataContext = new RepairRequestCreationWindowViewModel();
        }
    }
}
