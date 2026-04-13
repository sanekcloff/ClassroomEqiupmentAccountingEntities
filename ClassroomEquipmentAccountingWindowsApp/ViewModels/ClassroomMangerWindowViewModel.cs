using ClassroomEquipmentAccountingEntities.Models;
using ClassroomEquipmentAccountingWindowsApp.Core;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace ClassroomEquipmentAccountingWindowsApp.ViewModels
{
    public class ClassroomMangerWindowViewModel : ViewModelBase
    {
        private bool _isAdd = false;
        public ClassroomMangerWindowViewModel(Classroom? entity)
        {
            _isAdd = entity == null;
            Entity = entity ?? new Classroom();
            Managers = new ObservableCollection<User>(AppCore.Instance.AppDbContext.Users);
            SelectedManager = Managers.FirstOrDefault(u => u.Id == Entity.ManagerId) ?? Managers.FirstOrDefault();

            SaveCommand = new RelayCommand(_ => Save(), _ => CanSave);
        }

        public Classroom Entity { get => field; set => Set(ref field, in value, nameof(Entity)); }

        public string Number { get => Entity?.Number ?? string.Empty; set { if (Entity != null) { Entity.Number = value; OnNumberChanged(); } } }
        public string Specialization { get => Entity?.Specialization ?? string.Empty; set { if (Entity != null) { Entity.Specialization = value; OnPropertyChanged(nameof(Specialization)); } } }

        public ObservableCollection<User> Managers { get => field; set => Set(ref field, in value, nameof(Managers)); }
        public User SelectedManager
        {
            get => field;
            set
            {
                if (Set(ref field, in value, nameof(SelectedManager)))
                {
                    if (Entity != null && value != null) Entity.ManagerId = value.Id;
                }
            }
        }

        public RelayCommand SaveCommand { get; }

        public bool CanSave => _isAdd ? (AppCore.Instance.CurrentUser?.Permissions.HasFlag(Permission.AddClassrooms) ?? false)
                                    : (AppCore.Instance.CurrentUser?.Permissions.HasFlag(Permission.UpdateClassrooms) ?? false);

        private void OnNumberChanged()
        {
            OnPropertyChanged(nameof(Number));
        }

        private void Save()
        {
            if (!CanSave)
            {
                MessageBox.Show("Нет прав на сохранение кабинета", "Доступ запрещён", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                if (_isAdd)
                    AppCore.Instance.AppDbContext.Classrooms.Add(Entity);
                else
                    AppCore.Instance.AppDbContext.Classrooms.Update(Entity);

                AppCore.Instance.AppDbContext.SaveChanges();

                MessageBox.Show("Кабинет сохранён", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.DataContext == this)?.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
