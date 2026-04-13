using ClassroomEquipmentAccountingEntities.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace ClassroomEquipmentAccountingWindowsApp.ViewModels
{
    public class RepairRequestViewWindowViewModel : ViewModelBase
    {
        public RepairRequestViewWindowViewModel(RepairRequest request)
        {
            Request = request ?? throw new ArgumentNullException(nameof(request));
            InRequestEquipments = new ObservableCollection<Equipment>(Request.RepairRequestEquipments.Select(r => r.Equipment));
            CloseCommand = new RelayCommand(_ => CloseWindow());
        }

        public RepairRequest Request { get => field; set => Set(ref field, in value, nameof(Request)); }

        public ObservableCollection<Equipment> InRequestEquipments { get => field; set => Set(ref field, in value, nameof(InRequestEquipments)); }

        public RelayCommand CloseCommand { get; }

        private void CloseWindow()
        {
            Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.DataContext == this)?.Close();
        }
    }
}
