using ClassroomEquipmentAccountingEntities.Models;
using ClassroomEquipmentAccountingWindowsApp.ViewModels;
using System.Windows;

namespace ClassroomEquipmentAccountingWindowsApp.Views
{
    public partial class ClassroomMangerWindow : Window
    {
        public ClassroomMangerWindow()
        {
            InitializeComponent();
            DataContext = new ClassroomMangerWindowViewModel(null);
        }

        public ClassroomMangerWindow(Classroom classroom)
        {
            InitializeComponent();
            DataContext = new ClassroomMangerWindowViewModel(classroom);
        }
    }
}
