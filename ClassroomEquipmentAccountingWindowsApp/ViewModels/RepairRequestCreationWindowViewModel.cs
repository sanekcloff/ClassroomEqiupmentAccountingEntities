using ClassroomEquipmentAccountingEntities.Models;
using ClassroomEquipmentAccountingWindowsApp.Core;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace ClassroomEquipmentAccountingWindowsApp.ViewModels
{
    public class RepairRequestCreationWindowViewModel : ViewModelBase
    {
        public RepairRequestCreationWindowViewModel()
        {
            // Загружаем все оборудование как доступное (упростил логику по макету)
            AvailableEquipments = new ObservableCollection<Equipment>(AppCore.Instance.AppDbContext.Equipments);
            InRequestEquipments = new ObservableCollection<Equipment>();
            AddToRequestCommand = new RelayCommand(_ => AddSelected(), _ => SelectedAvailable != null && CanAdd);
            RemoveFromRequestCommand = new RelayCommand(_ => RemoveSelected(), _ => SelectedInRequest != null && CanAdd);
            SubmitCommand = new RelayCommand(_ => Submit(), _ => CanAdd && InRequestEquipments.Any());

            // Подписываемся на изменения коллекций, чтобы принудительно переоценить CanExecute
            InRequestEquipments.CollectionChanged += (_, __) => CommandManager.InvalidateRequerySuggested();
            AvailableEquipments.CollectionChanged += (_, __) => CommandManager.InvalidateRequerySuggested();
        }

        public ObservableCollection<Equipment> AvailableEquipments { get => field; set => Set(ref field, in value, nameof(AvailableEquipments)); }
        public ObservableCollection<Equipment> InRequestEquipments { get => field; set => Set(ref field, in value, nameof(InRequestEquipments)); }

        public Equipment SelectedAvailable
        {
            get => field;
            set
            {
                if (Set(ref field, in value, nameof(SelectedAvailable)))
                {
                    // обновляем CanExecute у команд
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }
        public Equipment SelectedInRequest
        {
            get => field;
            set
            {
                if (Set(ref field, in value, nameof(SelectedInRequest)))
                {
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        public RelayCommand AddToRequestCommand { get; }
        public RelayCommand RemoveFromRequestCommand { get; }
        public RelayCommand SubmitCommand { get; }

        public bool CanAdd => AppCore.Instance.CurrentUser?.Permissions.HasFlag(Permission.AddRequests) ?? false;

        private void AddSelected()
        {
            if (SelectedAvailable == null) return;
            InRequestEquipments.Add(SelectedAvailable);
            AvailableEquipments.Remove(SelectedAvailable);
            SelectedAvailable = null;
            CommandManager.InvalidateRequerySuggested();
        }

        private void RemoveSelected()
        {
            if (SelectedInRequest == null) return;
            AvailableEquipments.Add(SelectedInRequest);
            InRequestEquipments.Remove(SelectedInRequest);
            SelectedInRequest = null;
            CommandManager.InvalidateRequerySuggested();
        }

        private void Submit()
        {
            if (!CanAdd)
            {
                MessageBox.Show("У вас нет прав на создание заявки", "Доступ запрещён", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var request = new RepairRequest
                {
                    StartDate = DateTime.Now,
                    EndDate = null,
                    Description = string.Empty
                };

                AppCore.Instance.AppDbContext.RepairRequests.Add(request);
                AppCore.Instance.AppDbContext.SaveChanges();

                foreach (var eq in InRequestEquipments)
                {
                    var rre = new RepairRequestEquipment(request, eq);
                    AppCore.Instance.AppDbContext.RepairRequestEquipment.Add(rre);
                }

                AppCore.Instance.AppDbContext.SaveChanges();

                MessageBox.Show("Заявка успешно создана", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.DataContext == this)?.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении заявки: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
