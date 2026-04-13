using ClassroomEquipmentAccountingEntities.Models;
using ClassroomEquipmentAccountingWindowsApp.Core;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace ClassroomEquipmentAccountingWindowsApp.ViewModels
{
    public class RepairRequestManagerWindowViewModel : ViewModelBase
    {
        private readonly RepairRequest _requestEntity;

        public RepairRequestManagerWindowViewModel(RepairRequest request)
        {
            _requestEntity = request ?? throw new ArgumentNullException(nameof(request));

            InRequestEquipments = new ObservableCollection<Equipment>(_requestEntity.RepairRequestEquipments.Select(r => r.Equipment));
            AvailableEquipments = new ObservableCollection<Equipment>(AppCore.Instance.AppDbContext.Equipments.Where(e => !InRequestEquipments.Contains(e)));

            Description = _requestEntity.Description ?? string.Empty;

            SaveCommand = new RelayCommand(_ => Save(), _ => CanEdit);
        }

        public ObservableCollection<Equipment> AvailableEquipments { get => field; set => Set(ref field, in value, nameof(AvailableEquipments)); }
        public ObservableCollection<Equipment> InRequestEquipments { get => field; set => Set(ref field, in value, nameof(InRequestEquipments)); }

        public string Description { get => field; set => Set(ref field, in value, nameof(Description)); }

        public RelayCommand SaveCommand { get; }

        public bool CanEdit => AppCore.Instance.CurrentUser?.Permissions.HasFlag(Permission.UpdateRequests) ?? false;

        private void Save()
        {
            if (!CanEdit)
            {
                MessageBox.Show("У вас нет прав на редактирование заявки", "Доступ запрещён", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // Обновляем описание
                _requestEntity.Description = Description;

                // Удаляем существующие связи
                var existing = AppCore.Instance.AppDbContext.RepairRequestEquipment.Where(r => r.RepairRequest.Id == _requestEntity.Id).ToList();
                if (existing.Any())
                {
                    AppCore.Instance.AppDbContext.RepairRequestEquipment.RemoveRange(existing);
                }

                // Добавляем текущие связи
                foreach (var eq in InRequestEquipments)
                {
                    AppCore.Instance.AppDbContext.RepairRequestEquipment.Add(new RepairRequestEquipment(_requestEntity, eq));
                }

                AppCore.Instance.AppDbContext.RepairRequests.Update(_requestEntity);
                AppCore.Instance.AppDbContext.SaveChanges();

                MessageBox.Show("Заявка успешно сохранена", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.DataContext == this)?.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
