using ClassroomEquipmentAccountingEntities.Models;
using ClassroomEquipmentAccountingWindowsApp.Core;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace ClassroomEquipmentAccountingWindowsApp.ViewModels
{
    public class EquipmentMangerWindowViewModel : ViewModelBase
    {
        private bool _isAdd = false;
        public EquipmentMangerWindowViewModel(Equipment? entity)
        {
            _isAdd = entity == null;
            Entity = entity ?? new Equipment { CommissioningDate = DateTime.Now, WaranityEndDate = DateTime.Now };

            Classrooms = new ObservableCollection<Classroom>(AppCore.Instance.AppDbContext.Classrooms);
            Categories = new ObservableCollection<Category>(AppCore.Instance.AppDbContext.Categories);
            StatusValues = new ObservableCollection<Status>((Status[])Enum.GetValues(typeof(Status)));

            SelectedClassroom = Classrooms.FirstOrDefault(c => c.Id == Entity.ClassroomId) ?? Classrooms.FirstOrDefault();
            SelectedCategory = Categories.FirstOrDefault(c => c.Id == Entity.CategoryId) ?? Categories.FirstOrDefault();
            SelectedStatus = StatusValues.FirstOrDefault();

            SaveCommand = new RelayCommand(_ => Save(), _ => CanSave);
        }

        public Equipment Entity { get => field; set => Set(ref field, in value, nameof(Entity)); }

        public string SerialNumber { get => Entity?.SerialNumber ?? string.Empty; set { if (Entity != null) { Entity.SerialNumber = value; OnPropertyChanged(nameof(SerialNumber)); } } }
        public string InventoryNumber { get => Entity?.InventoryNumber ?? string.Empty; set { if (Entity != null) { Entity.InventoryNumber = value; OnPropertyChanged(nameof(InventoryNumber)); } } }
        public string Model { get => Entity?.Model ?? string.Empty; set { if (Entity != null) { Entity.Model = value; OnPropertyChanged(nameof(Model)); } } }

        public DateTime CommissioningDate { get => Entity?.CommissioningDate ?? DateTime.Now; set { if (Entity != null) { Entity.CommissioningDate = value; OnPropertyChanged(nameof(CommissioningDate)); } } }
        public DateTime WaranityEndDate { get => Entity?.WaranityEndDate ?? DateTime.Now; set { if (Entity != null) { Entity.WaranityEndDate = value; OnPropertyChanged(nameof(WaranityEndDate)); } } }

        public ObservableCollection<Classroom> Classrooms { get => field; set => Set(ref field, in value, nameof(Classrooms)); }
        public ObservableCollection<Category> Categories { get => field; set => Set(ref field, in value, nameof(Categories)); }
        public ObservableCollection<Status> StatusValues { get => field; set => Set(ref field, in value, nameof(StatusValues)); }

        public Classroom SelectedClassroom
        {
            get => field;
            set
            {
                if (Set(ref field, in value, nameof(SelectedClassroom)) && Entity != null && value != null)
                    Entity.ClassroomId = value.Id;
            }
        }
        public Category SelectedCategory
        {
            get => field;
            set
            {
                if (Set(ref field, in value, nameof(SelectedCategory)) && Entity != null && value != null)
                    Entity.CategoryId = value.Id;
            }
        }
        public Status SelectedStatus
        {
            get => field;
            set
            {
                if (Set(ref field, in value, nameof(SelectedStatus)) && Entity != null)
                    Entity.Status = value;
            }
        }

        public RelayCommand SaveCommand { get; }

        public bool CanSave => _isAdd ? (AppCore.Instance.CurrentUser?.Permissions.HasFlag(Permission.AddEquipments) ?? false)
                                    : (AppCore.Instance.CurrentUser?.Permissions.HasFlag(Permission.UpdateEquipments) ?? false);

        private void Save()
        {
            if (!CanSave)
            {
                MessageBox.Show("Нет прав на сохранение оборудования", "Доступ запрещён", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                if (_isAdd)
                    AppCore.Instance.AppDbContext.Equipments.Add(Entity);
                else
                    AppCore.Instance.AppDbContext.Equipments.Update(Entity);

                AppCore.Instance.AppDbContext.SaveChanges();

                MessageBox.Show("Оборудование сохранено", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.DataContext == this)?.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
