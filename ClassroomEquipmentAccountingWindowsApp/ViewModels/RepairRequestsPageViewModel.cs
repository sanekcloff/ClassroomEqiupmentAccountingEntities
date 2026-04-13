using ClassroomEquipmentAccountingEntities.Models;
using ClassroomEquipmentAccountingWindowsApp.Core;
using ClassroomEquipmentAccountingWindowsApp.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Collections.Generic;

namespace ClassroomEquipmentAccountingWindowsApp.ViewModels
{
    public class RepairRequestsPageViewModel : ViewModelBase
    {
        public RepairRequestsPageViewModel()
        {
            Items = new ObservableCollection<RepairRequest>(AppCore.Instance.AppDbContext.RepairRequests);
            ItemsView = CollectionViewSource.GetDefaultView(Items);

            // команды: добавление открывает окно создания
            AddCommand = new RelayCommand(_ => OpenCreationWindow(), _ => CanAdd);

            // DoubleClickCommand теперь принимает параметр — объект RepairRequest (надёжно при double-click)
            DoubleClickCommand = new RelayCommand(
                param => OnItemDoubleClick(param as RepairRequest),
                param => (param as RepairRequest) != null || SelectedItem != null);

            DeleteCommand = new RelayCommand(_ => DeleteSelected(), _ => SelectedItem != null && CanDelete);
            RefreshCommand = new RelayCommand(_ => Refresh());

            SortOptions = new List<string> { "По умолчанию", "Id", "Дата начала" };

            // Расширенные опции фильтрации по предметной области
            FilterOptions = new List<string> { "Все", "Выполненные", "В процессе" };

            CreateInventoryCommand = new RelayCommand(_ => CreateInventory(), _ => CanCreateInventory);

            SelectedSort = "По умолчанию";
            SelectedFilter = FilterOptions[0];
        }

        public ObservableCollection<RepairRequest> Items { get => field; set => Set(ref field, in value, nameof(Items)); }
        public ICollectionView ItemsView { get; }

        public RepairRequest SelectedItem { get => field; set => Set(ref field, in value, nameof(SelectedItem)); }

        public RelayCommand AddCommand { get; }
        public RelayCommand DoubleClickCommand { get; }
        public RelayCommand DeleteCommand { get; }
        public RelayCommand RefreshCommand { get; }
        public RelayCommand CreateInventoryCommand { get; }

        public List<string> SortOptions { get; }
        public List<string> FilterOptions { get; }

        public string SearchText
        {
            get => field;
            set
            {
                if (Set(ref field, in value, nameof(SearchText)))
                    ApplyFilter();
            }
        }

        public string SelectedSort
        {
            get => field;
            set
            {
                if (Set(ref field, in value, nameof(SelectedSort)))
                    ApplySort();
            }
        }

        public string SelectedFilter
        {
            get => field;
            set
            {
                if (Set(ref field, in value, nameof(SelectedFilter)))
                    ApplyFilter();
            }
        }

        public bool CanAdd => AppCore.Instance.CurrentUser?.Permissions.HasFlag(Permission.AddRequests) ?? false;
        public bool CanEdit => AppCore.Instance.CurrentUser?.Permissions.HasFlag(Permission.UpdateRequests) ?? false;
        public bool CanDelete => AppCore.Instance.CurrentUser?.Permissions.HasFlag(Permission.RemoveRequests) ?? false;
        public bool CanCreateInventory => AppCore.Instance.CurrentUser?.Permissions.HasFlag(Permission.CreatingInventory) ?? false;

        private void OpenCreationWindow()
        {
            new RepairRequestCreationWindow().ShowDialog();
            Refresh();
        }

        // Обработчик теперь принимает параметр — объект, на котором был двойной клик.
        private void OnItemDoubleClick(RepairRequest? item)
        {
            var target = item ?? SelectedItem;
            if (target == null) return;

            if (CanEdit)
            {
                // открыть окно управления (редактирование)
                new RepairRequestMangerWindow(target).ShowDialog();
            }
            else
            {
                // открыть окно просмотра
                new RepairRequestViewWindow(target).ShowDialog();
            }

            Refresh();
        }

        private void Refresh()
        {
            Items.Clear();
            foreach (var it in AppCore.Instance.AppDbContext.RepairRequests)
                Items.Add(it);
            ItemsView.Refresh();
        }

        private void ApplyFilter()
        {
            ItemsView.Filter = obj =>
            {
                if (obj is not RepairRequest r) return false;

                // Поиск по строке
                bool matchesSearch = string.IsNullOrWhiteSpace(SearchText)
                    || (r.ToString() ?? string.Empty).Contains(SearchText, StringComparison.OrdinalIgnoreCase);

                // Фильтрация по статусу выполнения
                bool matchesFilter = SelectedFilter == "Все"
                    || (SelectedFilter == "Выполненные" && r.EndDate != null)
                    || (SelectedFilter == "В процессе" && r.EndDate == null);

                return matchesSearch && matchesFilter;
            };
            ItemsView.Refresh();
        }

        private void ApplySort()
        {
            ItemsView.SortDescriptions.Clear();
            if (SelectedSort == "Id")
                ItemsView.SortDescriptions.Add(new SortDescription(nameof(RepairRequest.Id), ListSortDirection.Ascending));
            else if (SelectedSort == "Дата начала")
                ItemsView.SortDescriptions.Add(new SortDescription(nameof(RepairRequest.StartDate), ListSortDirection.Ascending));
        }

        private void DeleteSelected()
        {
            if (SelectedItem == null) return;

            var result = MessageBox.Show(
                $"Вы действительно хотите удалить заявку \"{SelectedItem}\"?",
                "Подтверждение удаления",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result != MessageBoxResult.Yes) return;

            try
            {
                AppCore.Instance.AppDbContext.RepairRequests.Remove(SelectedItem);
                AppCore.Instance.AppDbContext.SaveChanges();
                Items.Remove(SelectedItem);
                SelectedItem = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CreateInventory()
        {
            if (SelectedItem == null)
            {
                MessageBox.Show("Выберите заявку для создания инвентаря", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            MessageBox.Show($"Создание инвентаря для заявки {SelectedItem.Id}", "Инвентарь", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
