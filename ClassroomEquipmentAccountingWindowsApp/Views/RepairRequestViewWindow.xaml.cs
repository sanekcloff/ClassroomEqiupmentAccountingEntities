using ClassroomEquipmentAccountingEntities.Models;
using ClassroomEquipmentAccountingWindowsApp.ViewModels;
using System.Windows;

namespace ClassroomEquipmentAccountingWindowsApp.Views
{
    /// <summary>
    /// Логика взаимодействия для RepairRequestViewWindow.xaml
    /// </summary>
    public partial class RepairRequestViewWindow : Window
    {
        public RepairRequestViewWindow(RepairRequest request)
        {
            InitializeComponent();
            DataContext = new RepairRequestViewWindowViewModel(request);
        }
    }
}
