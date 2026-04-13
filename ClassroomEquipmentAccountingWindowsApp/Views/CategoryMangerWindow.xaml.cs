using ClassroomEquipmentAccountingEntities.Models;
using ClassroomEquipmentAccountingWindowsApp.ViewModels;
using System.Windows;

namespace ClassroomEquipmentAccountingWindowsApp.Views
{
    public partial class CategoryMangerWindow : Window
    {
        public CategoryMangerWindow()
        {
            InitializeComponent();
            DataContext = new CategoryMangerWindowViewModel(null);
        }

        public CategoryMangerWindow(Category category)
        {
            InitializeComponent();
            DataContext = new CategoryMangerWindowViewModel(category);
        }
    }
}
