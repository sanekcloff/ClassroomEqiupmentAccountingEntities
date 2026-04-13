using Azure.Core;
using ClassroomEquipmentAccountingEntities.Models;
using ClassroomEquipmentAccountingWindowsApp.ViewModels;
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
using System.Windows.Shapes;

namespace ClassroomEquipmentAccountingWindowsApp.Views
{
    /// <summary>
    /// Логика взаимодействия для RepairRequestMangerWindow.xaml
    /// </summary>
    public partial class RepairRequestMangerWindow : Window
    {
        public RepairRequestMangerWindow(RepairRequest entity)
        {
            InitializeComponent();
            DataContext = new RepairRequestManagerWindowViewModel(entity);
        }
    }
}
